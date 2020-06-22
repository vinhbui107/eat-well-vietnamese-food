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
    public class ProductOptionsSvc : GenericSvc<ProductOptionsRep, ProductOptions>
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
        public override SingleRsp Update(ProductOptions m)
        {
            var res = new SingleRsp();

            var m1 = m.ProductId > 0 ? _rep.Read(m.ProductId) : _rep.Read(m.Price);
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
        public ProductOptionsSvc() { }


        #endregion
        //===========================================================
        //===========================================================
        #region -- Create Product Options --

        public SingleRsp CreateProductOptions(ProductOptionsReq po)
        {
            var res = new SingleRsp();
            ProductOptions productoption = new ProductOptions();
            productoption.OptionId = po.id;
            productoption.ProductId = po.product_id;
            productoption.Price = po.price;
            res = _rep.CreateProductOptions(productoption);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================
        #region -- Update Product Options --

        public SingleRsp UpdateProductOptions(int id, ProductOptionsReq po)
        {
            var res = new SingleRsp();
            ProductOptions productoption = new ProductOptions();
            productoption.OptionId = po.id;
            productoption.ProductId = po.product_id;
            productoption.Price = po.price;
            res = _rep.UpdateProductOptions(productoption);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

      

        //===========================================================
        //===========================================================

        #region -- Get Product Options With Pagination --
        public object GetAllProductOptionsWithPagination(int page, int size)
        {
            var pos = from po in _rep.Context.ProductOptions
                      join p in _rep.Context.Products on po.ProductId equals p.ProductId
                      join o in _rep.Context.Options on po.OptionId equals o.OptionId
                      select new
                      {
                          product_id = po.ProductId,
                          product_name = p.ProductName,
                          id = o.OptionId,
                          option_name = o.OptionName,
                          price = po.Price
                  
                      };

            var offset = (page - 1) * size;
            var total = pos.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = pos.OrderBy(x => x.product_id).Skip(offset).Take(size).ToList();
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

    }
}
