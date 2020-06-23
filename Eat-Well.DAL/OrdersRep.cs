using Eat_Well.Common.DAL;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Eat_Well.DAL
{
    public class OrdersRep: GenericRep<EatWellDBContext, Orders>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Orders Read(int id)
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
        public SingleRsp CreateOrders(Orders ord)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Orders.Add(ord);
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
        public SingleRsp UpdateOrders(Orders ord)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Orders.Update(ord);
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
        public bool DeleteOrders(int Id)
        {
            EatWellDBContext db = new EatWellDBContext();
            Orders orders = db.Orders.FirstOrDefault(x => x.OrderId == Id);
            if (orders == null) return false;
            db.Orders.Remove(orders);
            db.SaveChangesAsync();
            return true;
        }
        #endregion
        //=================================================================
        //=================================================================
        //=================================================================
        #region -- get Revenue With Month --
        public object getRevenueWithMonth(int month, int year)
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)Context.Database.GetDbConnection();
            if (cmn.State == ConnectionState.Closed)
                cmn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmt = cmn.CreateCommand();
                cmt.CommandText = "SpSel_getRevenueWithMonth";
                cmt.CommandType = CommandType.StoredProcedure;
                cmt.Parameters.AddWithValue("@month", month);
                cmt.Parameters.AddWithValue("@year", year);
                da.SelectCommand = cmt;
                da.Fill(ds);
                //kiem tra
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            OrderId = row["OrderId"],
                            IsCompleted = row["IsCompleted"],
                            OrderDate = row["OrderDate"],
                            Revenue = row["Revenue"]
                        };
                        res.Add(x);
                    }

                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }
        #endregion

    }
}
