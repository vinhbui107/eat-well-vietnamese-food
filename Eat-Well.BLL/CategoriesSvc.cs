using System;
using System.Collections.Generic;
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

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public CategoriesSvc() { }


        #endregion

        #region -- Create Product --

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
        #region -- Update Product --

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
        #region -- Delete Product --

        public SingleRsp RemoveCategory(CategoriesReq cate)
        {
            var res = new SingleRsp();
            Categories categories = new Categories();
            categories.CategoryId = cate.CategoryId;
            categories.CategoryName = cate.CategoryName;
            categories.CategorySlug = cate.CategorySlug;
            res = _rep.RemoveCategory(categories);
               return res;
        }
        #endregion


    }
}
