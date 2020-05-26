using Eat_Well.Common.BLL;
using Eat_Well.Common.Req;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL;
using Eat_Well.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eat_Well.BLL
{
    public class OrdersSvc: GenericSvc<OrdersRep, Orders>
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
        public override SingleRsp Update(Orders m)
        {
            var res = new SingleRsp();

            var m1 = m.OrderId > 0 ? _rep.Read(m.OrderId) : _rep.Read(m.OrderDescription);
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
        public OrdersSvc() { }


        #endregion
        //===========================================================
        //===========================================================
        #region -- Create Orders --

        public SingleRsp CreateOrders(OrdersReq ord)
        {
            var res = new SingleRsp();
            Orders orders = new Orders();
            orders.OrderId = ord.OrderId;
            orders.UserId = ord.UserId;
            orders.OrderTotal = ord.OrderTotal;
            orders.OrderPhone = ord.OrderPhone;
            orders.OrderDate = ord.OrderDate;
            orders.OrderStatus = ord.OrderStatus;
            orders.ShippingAddress = ord.ShippingAddress;
            orders.OrderDescription = ord.OrderDescription;
            res = _rep.CreateOrders(orders);
            return res;
        }
        #endregion

        //===========================================================
        //===========================================================
        #region -- Update Product --

        public SingleRsp UpdateOrders(OrdersReq ord)
        {
            var res = new SingleRsp();
            Orders orders = new Orders();
            orders.OrderId = ord.OrderId;
            orders.UserId = ord.UserId;
            orders.OrderTotal = ord.OrderTotal;
            orders.OrderPhone = ord.OrderPhone;
            orders.OrderDate = ord.OrderDate;
            orders.OrderStatus = ord.OrderStatus;
            orders.ShippingAddress = ord.ShippingAddress;
            orders.OrderDescription = ord.OrderDescription;
            res = _rep.UpdateOrders(orders);
            return res;
        }
        #endregion
    }
}
