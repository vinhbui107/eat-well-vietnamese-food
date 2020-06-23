using System;
using System.Collections.Generic;
using System.Linq;

using Eat_Well.Common.BLL;
using Eat_Well.Common.Req;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL.Models;
using Eat_Well.DAL;



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

        #region -- Get Product By ID -- 
        public object GetProductById(int id)
        {

            var product = from p in _rep.Context.Products
                          where p.ProductId == id
                          select new
                          {
                              id = p.ProductId,
                              name = p.ProductName,
                              /*This category below will return
                               *
                               * "category:" {
                               *      "id": id,
                               *      "name": name,
                               * } 
                               * 
                               */
                              category = (from c in _rep.Context.Categories
                                          where c.CategoryId == p.CategoryId
                                          select new
                                          {
                                              id = c.CategoryId,
                                              name = c.CategoryName
                                          }).FirstOrDefault(),
                              photo = p.Photo,
                              description = p.Description,
                              slug = p.ProductSlug,
                              is_active = p.IsActive,
                              
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
                                         where p.ProductId == po.ProductId
                                         select new
                                         {
                                             id = o.OptionId,
                                             name = o.OptionName,
                                             price = po.Price
                                         }).ToList()
                            };
            return product;
        }
        # endregion
        //===========================================================
        //===========================================================

        #region -- Get Product With Pagination --
        public object GetAllProductWithPagination(int page, int size)
        {
            var products = from p in _rep.Context.Products
                          select new
                          {
                              id = p.ProductId,
                              name = p.ProductName,
                              /*This category below will return
                               *
                               * "category:" {
                               *      "id": id,
                               *      "name": name,
                               * } 
                               * 
                               */
                              category = (from c in _rep.Context.Categories
                                          where c.CategoryId == p.CategoryId
                                          select new
                                          {
                                              id = c.CategoryId,
                                              name = c.CategoryName
                                          }).FirstOrDefault(),
                              photo = p.Photo,
                              description = p.Description,
                              slug = p.ProductSlug,
                              is_active = p.IsActive,

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
                                         where p.ProductId == po.ProductId
                                         select new
                                         {
                                             id = o.OptionId,
                                             name = o.OptionName,
                                             price = po.Price
                                         }).ToList()
                          };

            // pagination
            var offset = (page - 1) * size;
            var total = products.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = products.OrderBy(x => x.id).Skip(offset).Take(size).ToList();
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

        #region -- Create Product --

        public SingleRsp CreateProduct(ProductsReq pro)
        {
            var res = new SingleRsp();
            Products product = new Products();

            product.CategoryId = pro.category_id;
            product.ProductName = pro.name;
            product.Photo = pro.photo;
            product.Description = pro.description;
            product.ProductSlug = pro.slug;
            product.IsActive = pro.is_active;

            // we must to save a new product before.
            // if we don't, we will not have productID to do anything.
            res = _rep.CreateProduct(product);

            // ProductsReq pro have Options is a list.
            // so we get those options and store it into database.
            foreach (var po in pro.options)
            {
                using (var context = new EatWellDBContext())
                {
                    using (var tran = context.Database.BeginTransaction())
                    {
                        try
                        {
                            ProductOptions product_option = new ProductOptions();

                            product_option.ProductId = product.ProductId;
                            product_option.OptionId = po.id;
                            product_option.Price = po.price;

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

        public SingleRsp UpdateProduct(int id, ProductsReq pro)
        {
            var res = new SingleRsp();

            var product = All.FirstOrDefault(x => x.ProductId.Equals(id));

            product.CategoryId = pro.category_id;
            product.ProductName = pro.name;
            product.Photo = pro.photo;
            product.Description = pro.description;
            product.ProductSlug = pro.slug;
            product.IsActive = pro.is_active;

            foreach (var po in pro.options)
            {
                EatWellDBContext db = new EatWellDBContext();
                var product_option = new ProductOptions();

                product_option.ProductId = product.ProductId;
                product_option.OptionId = po.id;
                product_option.Price = po.price;
                
                db.ProductOptions.Update(product_option);
                db.SaveChangesAsync();
            }
                
            res = _rep.UpdateProduct(product);

            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Delete Product --
        public object DeleteProduct(int id)
        {
            return _rep.DeleteProduct(id);
        }
        #endregion
        //===========================================================
        //===========================================================

        #region --Searh Product--
        public object searchProductWithPagination(string key, int page, int size)
        {
            return _rep.searchProductWithPagination(key, page, size);
        }
        #endregion
    }
}