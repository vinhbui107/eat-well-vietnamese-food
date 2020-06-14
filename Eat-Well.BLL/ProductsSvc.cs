using Eat_Well.Common.BLL;
using Eat_Well.Common.Req;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL;
using Eat_Well.DAL.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Eat_Well.BLL
{
    public class ProductsSvc : GenericSvc<ProductsRep, Products>
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
        public override SingleRsp Update(Products m)
        {
            var res = new SingleRsp();

            var m1 = m.ProductId > 0 ? _rep.Read(m.ProductId) : _rep.Read(m.Description);
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

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public ProductsSvc() { }


        #endregion
        //===========================================================
        //===========================================================

        #region -- Create Product --

        public SingleRsp CreateProduct(ProductsReq pro)
        {
            var res = new SingleRsp();
            Products product = new Products();

            product.CategoryId = pro.CategoryId;
            product.ProductName = pro.ProductName;
            product.Photo = pro.Photo;
            product.Description = pro.Description;
            product.ProductSlug = pro.ProductSlug;
            product.IsActive = pro.IsActive;

            // we must to save a new product before.
            // if we don't, we will not have productID to do anything.
            res = _rep.CreateProduct(product);

            // ProductsReq pro have Options is a list.
            // so we get those options and store it into database.
            foreach (var po in pro.Options)
            {
                using (var context = new EatWellDBContext())
                {
                    using (var tran = context.Database.BeginTransaction())
                    {
                        try
                        {
                            ProductOptions product_option = new ProductOptions();
                            product_option.ProductId = product.ProductId;
                            product_option.OptionId = po.OptionId;
                            product_option.Price = po.Price;

                            // add a new record in to ProductOptions table
                            var t = context.ProductOptions.Add(product_option);

                            context.SaveChanges();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            res.SetError(ex.StackTrace);
                        }
                    }
                }
            }

            return res;
        }
        #endregion
        //===========================================================
        //===========================================================
        
        #region -- Update Product --

        public SingleRsp UpdateProduct(ProductsReq pro)
        {
            var res = new SingleRsp();

            var product = All.Where(x => x.ProductId == pro.ProductId).FirstOrDefault();

            if  (product != null)
            {
                product.CategoryId = pro.CategoryId;
                product.ProductName = pro.ProductName;
                product.Photo = pro.Photo;
                product.Description = pro.Description;
                product.ProductSlug = pro.ProductSlug;
                product.IsActive = pro.IsActive;

                res = _rep.UpdateProduct(product);

                foreach (var po in product.ProductOptions)
                {
                    EatWellDBContext db = new EatWellDBContext();
                    var productoption = new ProductOptions();

                    productoption.ProductId = product.ProductId;
                    productoption.OptionId = po.OptionId;
                    productoption.Price = po.Price;

                    db.ProductOptions.Update(productoption);
                    db.SaveChangesAsync();
                }
            }
            else
            {
                
            }

            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Delete Product --
        public object DeleteProduct(int Id)
        {
            return _rep.DeleteProduct(Id);
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Get Product With Pagination --
        public object GetAllProductWithPagination(int page, int size)
        {
            var products = All.Where(x => x.ProductId != null)
                .Join(_rep.Context.Categories, a => a.CategoryId, b => b.CategoryId, (a, b) => new
                {
                    id = a.ProductId,
                    name = a.ProductName,
                    /*This category below will return
                     *
                     * "category:" {
                     *      "id": id,
                     *      "name": name,
                     * } 
                     * 
                     */
                    category = (from c in _rep.Context.Categories
                                where c.CategoryId == a.CategoryId
                                select new
                                {
                                    id = c.CategoryId,
                                    name = c.CategoryName
                                }).FirstOrDefault(),
                    photo = a.Photo,
                    description = a.Description,
                    slug = a.ProductSlug,
                    is_active = a.IsActive,

                    /*This options below will return
                     *
                     * "options:"[
                     *              {
                     *                  "id": id,
                     *                  "name": name,
                     *                  "price": price
                     *              },
                     *              {
                     *                  "id": id,
                     *                  "name": name,
                     *                  "price": price
                     *              },
                     *           ]
                     * 
                     */
                    options = (from o in _rep.Context.Options
                               join po in _rep.Context.ProductOptions on o.OptionId equals po.OptionId
                               where a.ProductId == po.ProductId
                               select new
                               {
                                   id = o.OptionId,
                                   name = o.OptionName,
                                   price = po.Price
                               }).ToList()
                }).OrderBy(x => x.id);

            // pagination
            var offset = (page - 1) * size;
            var total = products.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = products.OrderBy(x => x.id).Skip(offset).Take(size).ToList();
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

        #region -- Get Product By ID -- 
        public object GetProductById(int id)
        {

            var product = All.Where(x => x.ProductId == id)
                .Join(_rep.Context.Categories, a => a.CategoryId, b => b.CategoryId, (a, b) => new
                {
                    id = a.ProductId,
                    name = a.ProductName,

                    /*This category below will return
                     *
                     * "category:" {
                     *      "id": id,
                     *      "name": name,
                     * } 
                     * 
                     */
                    category = (from c in _rep.Context.Categories
                                where c.CategoryId == a.CategoryId
                                select new { 
                                    id = c.CategoryId,
                                    name = c.CategoryName
                                }).FirstOrDefault(),
                    photo = a.Photo,
                    description = a.Description,
                    slug = a.ProductSlug,
                    is_active = a.IsActive,

                    /*This options below will return
                     *
                     * "options:"[
                     *              {
                     *                  "id": id,
                     *                  "name": name,
                     *                  "price": price
                     *              },
                     *              {
                     *                  "id": id,
                     *                  "name": name,
                     *                  "price": price
                     *              },
                     *           ]
                     * 
                     */
                    options = (from o in _rep.Context.Options
                               join po in _rep.Context.ProductOptions on o.OptionId equals po.OptionId
                               where a.ProductId == po.ProductId
                               select new
                               {
                                   id = o.OptionId,
                                   name = o.OptionName,
                                   price = po.Price
                               }).ToList()
               }).OrderBy(x => x.id);

            return product;
        }
        #endregion
        //===========================================================
        //===========================================================
    }
}