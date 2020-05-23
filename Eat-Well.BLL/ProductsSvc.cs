using Eat_Well.Common.BLL;
using Eat_Well.Common.Req;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL;
using Eat_Well.DAL.Models;
using System;
using System.Collections.Generic;
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

        public SingleRsp RemoveProduct(ProductsReq pro)
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
            res = _rep.RemoveProduct(products);
            return res;
        }
        #endregion
    }
}

