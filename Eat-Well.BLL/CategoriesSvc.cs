using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using Eat_Well.BLL;
using Eat_Well.Common.Rsp;
using Eat_Well.Common.BLL;
using Eat_Well.Common.Req;

namespace Eat_Well.BLL
{
    using DAL;
    using DAL.Models;

    public class CategoriesSvc: GenericSvc<CategoriesRep, Categories>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);

            res.Data = m;

            return res;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public override SingleRsp Update(Categories m)
        {
            var res = new SingleRsp();

            var m1 = m.CategoryId > 0 ? _rep.Read(m.CategoryId) : _rep.Read(m.CategoryName);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }

            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Methods --
        /// <summary>
        /// Initialize
        /// </summary>
        public CategoriesSvc() { }

        #endregion
        //===========================================================
        //===========================================================

        #region -- Create Category --
        public SingleRsp CreateCategory(CategoriesReq cate)
        {
            var res = new SingleRsp();
            Categories categories = new Categories();
            categories.CategoryId = cate.id;
            categories.CategoryName = cate.name;
            categories.CategorySlug = cate.slug;
            res = _rep.CreateCategory(categories);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Update Category --
        public SingleRsp UpdateCategory(int id, CategoriesReq cate)
        {
            var res = new SingleRsp();
            var cates = All.FirstOrDefault(x => x.CategoryId.Equals(id));
            cates.CategoryId = cate.id;
            cates.CategoryName = cate.name;
            cates.CategorySlug = cate.slug;
            res = _rep.UpdateCategory(cates);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Delete Category --
        public object DeleteCategory(int Id)
        {
            return _rep.DeleteCategory(Id);
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Get Categories With Pagination --
        public object GetAllCategoriesWithPagination(int page, int size)
        {
            var cate = from c in _rep.Context.Categories
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                       where c.CategoryId != null
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                       select new
                       {
                           id = c.CategoryId,
                           name = c.CategoryName,
                           slug = c.CategorySlug,
                       };
            var offset = (page - 1) * size;
            var total = cate.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = cate.OrderBy(x => x.id).Skip(offset).Take(size).ToList();
            var res = new
            {
                data = data,
                total_record = total,
                total_page = totalpage,
                page = page,
                size = size
            };

            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Get Category By ID -- 
        public object GetCategoryById(int id)
        {

            var user = from c in _rep.Context.Categories
                       where c.CategoryId == id
                       select new
                       {
                           id = c.CategoryId,
                           name = c.CategoryName,
                           slug = c.CategorySlug,
                           products = (from a in _rep.Context.Categories
                                       join p in _rep.Context.Products on a.CategoryId equals p.CategoryId
                                       where c.CategoryId == p.CategoryId
                                       select new
                                       {
                                           id = p.ProductId,
                                           name = p.ProductName,
                                           descripton = p.Description,
                                           //Get category with products
                                           /*Like this
                                           *  
                                           *                                                                           
                                           * "category:" {
                                           *               "id": id,
                                           *               "name": name,
                                           *                "products": [
                                           *                             {
                                           *                               "id":
                                           *                               "name":
                                           *                               "descripton":
                                           *                             },
                                           *
                                           *                             {
                                           *                               "id": ,
                                           *                               "name": "",
                                           *                               "descripton": "",
                                           *                             },
                                           *                           ]
                                           *             }
                                           *//////////////////////////////////////

                                           options = (from o in _rep.Context.Options
                                                      join po in _rep.Context.ProductOptions on o.OptionId equals po.OptionId
                                                      where po.ProductId == p.ProductId
                                                      select new
                                                      {
                                                        id = o.OptionId,
                                                        name = o.OptionName,
                                                        price = po.Price
                                                      }).ToList()
                                         }).ToList()
                                                                              
                       };
                                          //Get Options
                                          /*Like this
                                           * 
                                           * 
                                           *..........
                                           * "products": [
                                           *             {
                                           *               "id": 
                                           *               "name": 
                                           *               "descripton": 
                                           *               "options" : [
                                           *                             {
                                           *                               "id":
                                           *                               "name":
                                           *                               "price":
                                           *                             },
                                           *
                                           *                             {
                                           *                               "id":
                                           *                               "name":
                                           *                               "price":
                                           *                             }
                                           *                           ]
                                           *             },
                                           *           ]
                                           *//////////////////////////////

            return user;



        }
        #endregion
        //===========================================================
        //===========================================================
    }
}
