using Eat_Well.Common.BLL;
using Eat_Well.Common.Req;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL;
using Eat_Well.DAL.Models;
using System;
using System.Collections.Generic;
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
            Products products = new Products();
            products.ProductId = pro.ProductId;
            products.CategoryId = pro.CategoryId;
            products.ProductName = pro.ProductName;
            products.Photo = pro.Photo;
            products.Description = pro.Description;
            products.ProductSlug = pro.ProductSlug;
            products.IsActive = pro.IsActive;
            res = _rep.CreateProduct(products);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================
        #region -- Update Product --

        public SingleRsp UpdateProduct(ProductsReq pro)
        {
            var res = new SingleRsp();
            Products products = new Products();
            products.ProductId = pro.ProductId;
            products.CategoryId = pro.CategoryId;
            products.ProductName = pro.ProductName;
            products.Photo = pro.Photo;
            products.Description = pro.Description;
            products.ProductSlug = pro.ProductSlug;
            products.IsActive = pro.IsActive;
            res = _rep.UpdateProduct(products);
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
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            var pro = All.Where(x => x.ProductId != null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
               .Join(_rep.Context.Categories, a => a.CategoryId, b => b.CategoryId, (a, b) => new
               {
                   a.ProductId,
                   a.CategoryId,
                   a.ProductName,
                   a.Photo,
                   a.Description,
                   a.ProductSlug,
                   a.IsActive,
                   CategoryName = b.CategoryName,
               })
               //.Join(_rep.Context.ProductOptions, a => a.ProductId, b => b.ProductId, (a, b) => new
               //{
               //    a.ProductId,
               //    a.CategoryId,
               //    a.ProductName,
               //    a.Photo,
               //    a.Description,
               //    a.ProductSlug,
               //    a.IsActive,
               //    a.CategoryName,
               //    Price = b.Price,
               //})
               .OrderBy(x => x.ProductId);

            var offset = (page - 1) * size;
            var total = pro.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = pro.OrderBy(x => x.ProductId).Skip(offset).Take(size).ToList();
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

        public object GetProductById(int id)
        {
            var pro = All.Where(x => x.ProductId == id)
                .Join(_rep.Context.Categories, a => a.CategoryId, b => b.CategoryId, (a, b) => new
                {
                    a.ProductId,
                    a.CategoryId,
                    a.ProductName,
                    a.Photo,
                    a.Description,
                    a.ProductSlug,
                    a.IsActive,
                    CategoryName = b.CategoryName,
                })
               .Join(_rep.Context.ProductOptions, a => a.ProductId, b => b.ProductId, (a, b) => new
               {
                   a.ProductId,
                   a.CategoryId,
                   a.ProductName,
                   a.Photo,
                   a.Description,
                   a.ProductSlug,
                   a.IsActive,
                   a.CategoryName,
                   OptionId= b.OptionId,
                   Price = b.Price,
               })
                .Join(_rep.Context.Options, a => a.OptionId, b => b.OptionId, (a, b) => new
                {
                    a.ProductId,
                    a.CategoryId,
                    a.ProductName,
                    a.Photo,
                    a.Description,
                    a.ProductSlug,
                    a.IsActive,
                    a.CategoryName,
                    a.OptionId,
                    a.Price,
                    opti = b.OptionName,
                }).OrderBy(x => x.ProductId);
            return pro;
        }
    }
}