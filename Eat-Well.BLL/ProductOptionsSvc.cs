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
            productoption.OptionId = po.OptionId;
            productoption.ProductId = po.ProductId;
            productoption.Price = po.Price;
            res = _rep.CreateProductOptions(productoption);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================
        #region -- Update Product Options --

        public SingleRsp UpdateProductOptions(ProductOptionsReq po)
        {
            var res = new SingleRsp();
            ProductOptions productoption = new ProductOptions();
            productoption.OptionId = po.OptionId;
            productoption.ProductId = po.ProductId;
            productoption.Price = po.Price;
            res = _rep.UpdateProductOptions(productoption);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Delete Product Options --
        public object DeleteProductOptions(int PoId, int OpId)
        {
            return _rep.DeleteProductOptions(PoId,OpId);
        }
        #endregion

        //===========================================================
        //===========================================================

        #region -- Get Product Options With Pagination --
        public object GetAllProductOptionsWithPagination(int page, int size)
        {
            EatWellDBContext db = new EatWellDBContext();
            var pro = db.ProductOptions.ToList();
            var offset = (page - 1) * size;
            var total = pro.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = pro.OrderBy(x => x.OptionId).Skip(offset).Take(size).ToList();
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
