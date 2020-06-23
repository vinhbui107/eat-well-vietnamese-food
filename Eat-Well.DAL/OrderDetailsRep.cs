using Eat_Well.Common.DAL;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Eat_Well.DAL
{
    public class OrderDetailsRep: GenericRep<EatWellDBContext, OrderDetails>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override OrderDetails Read(int id)
        {
            var res = All.FirstOrDefault(p => p.ProductId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.ProductId == id);
            m = base.Delete(m); //TODO
            return m.ProductId;
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>

        #endregion

        //=================================================================
        //=================================================================

        #region -- Create OrderDetails --
        public SingleRsp CreateOrderDetails(OrderDetails od)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.OrderDetails.Add(od);
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
            return res;
        }

        #endregion
        //=================================================================
        //=================================================================

        #region -- Update OrderDetails --
        public SingleRsp UpdateOrderDetails(OrderDetails od)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.OrderDetails.Update(od);
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
            return res;
        }

        #endregion
        //=================================================================
        //=================================================================
        #region -- Delete OrderDetails --
        public bool DeleteOrderDetails(int ordId, int proId)
        {
            EatWellDBContext db = new EatWellDBContext();
            OrderDetails orders = db.OrderDetails.FirstOrDefault(x => x.OrderId == ordId && x.ProductId == proId);
            if (orders == null) return false;
            db.OrderDetails.Remove(orders);
            db.SaveChangesAsync();
            return true;
        }
        #endregion

        //=================================================================
        //=================================================================
    }
}
