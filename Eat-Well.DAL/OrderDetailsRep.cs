using Eat_Well.Common.DAL;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eat_Well.DAL
{
    using Eat_Well.DAL.Models;
    public class OrderDetailsRep : GenericRep<EatWellDBContext, OrderDetails>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override OrderDetails Read(int id)
        {
            var res = All.FirstOrDefault(p => p.OrderId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.OrderId == id);
            m = base.Delete(m); //TODO
            return m.OrderId;
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>

        #endregion

        //=================================================================
        //=================================================================
        //=================================================================
        #region -- Create Orders --
        public SingleRsp CreateOrdersDetails(OrderDetails ords)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.OrderDetails.Add(ords);
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
        //=================================================================
        #region -- Update Orders --
        public SingleRsp UpdateOrderDetails(OrderDetails ords)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.OrderDetails.Update(ords);
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
        //=================================================================
        #region -- Delete Orders --
        public bool DeleteOrderDetails(int OrdId, int ProId)
        {
            EatWellDBContext db = new EatWellDBContext();
            OrderDetails orderdetails = db.OrderDetails.FirstOrDefault(x => x.OrderId == OrdId && x.ProductId == ProId);
            if (orderdetails == null) return false;
            db.OrderDetails.Remove(orderdetails);
            db.SaveChangesAsync();
            return true;
        }
        #endregion
        //=================================================================
        //=================================================================
        //=================================================================
    }
}