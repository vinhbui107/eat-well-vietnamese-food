using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Eat_Well.Common.DAL;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL.Models;


namespace Eat_Well.DAL
{
    public class CategoriesRep: GenericRep<EatWellDBContext, Categories>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Categories Read(int id)
        {
            var res = All.FirstOrDefault(p => p.CategoryId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.CategoryId == id);
            m = base.Delete(m); //TODO
            return m.CategoryId;
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>

        #region MyRegion
        public SingleRsp CreateCategory(Categories cate)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Categories.Add(cate);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        res.SetError(e.StackTrace);
                    }
                }
            }

            return res;
        }

        #endregion
        //=================================================================
        //=================================================================
        //=================================================================

        #region MyRegion

        public SingleRsp UpdateCategory(Categories cate)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Categories.Update(cate);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        res.SetError(e.StackTrace);
                    }
                }
            }

            return res;
        }
        #endregion
        //=================================================================
        //=================================================================
        //=================================================================

        #region -- Delete Category --
        
        public bool DeleteCategory(int Id)
        {
            EatWellDBContext db = new EatWellDBContext();
            Categories cate = db.Categories.FirstOrDefault(x => x.CategoryId == Id);
            if (cate == null) return false;
            db.Categories.Remove(cate);
            db.SaveChangesAsync();
            return true;
        }
        #endregion
        #endregion


    }
}
