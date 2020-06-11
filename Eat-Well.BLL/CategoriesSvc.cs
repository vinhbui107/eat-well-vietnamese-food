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
            categories.CategoryId = cate.CategoryId;
            categories.CategoryName = cate.CategoryName;
            categories.CategorySlug = cate.CategorySlug;
            res = _rep.CreateCategory(categories);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Update Category --
        public SingleRsp UpdateCategory(CategoriesReq cate)
        {
            var res = new SingleRsp();
            Categories categories = new Categories();
            categories.CategoryId = cate.CategoryId;
            categories.CategoryName = cate.CategoryName;
            categories.CategorySlug = cate.CategorySlug;
            res = _rep.UpdateCategory(categories);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Delete Category --
        public bool DeleteCategory(int Id)
        {
            EatWellDBContext db = new EatWellDBContext();
            Categories category = db.Categories.FirstOrDefault(x => x.CategoryId == Id);
            if (category == null) return false;
            db.Categories.Remove(category);
            db.SaveChangesAsync();
            return true;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Get Categories With Pagination --
        public object GetAllCategoriesWithPagination(int page, int size)
        {
            EatWellDBContext db = new EatWellDBContext();
            var cate = db.Categories.ToList();
            var offset = (page - 1) * size;
            var total = cate.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = cate.OrderBy(x => x.CategoryId).Skip(offset).Take(size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalpage,
                Page = page,
                Size = size
            };

            return res;
        }
        #endregion
        //===========================================================
        //===========================================================
    }
}
