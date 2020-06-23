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
            orders.OrderId = ord.id;
            orders.UserId = ord.user_id;
            orders.OrderTotal = ord.order_total;
            orders.OrderPhone = ord.phone;
            orders.OrderDate = ord.date;
            orders.IsCompleted = ord.is_completed;
            orders.ShippingAddress = ord.shipping_address;
            orders.OrderDescription = ord.description;
            res = _rep.CreateOrders(orders);
            return res;
        }
        #endregion

        //===========================================================
        //===========================================================
        #region -- Update Orders --

        public SingleRsp UpdateOrders(int id, OrdersReq ord)
        {
            var res = new SingleRsp();
            Orders orders = new Orders();
            orders.OrderId = ord.id;
            orders.UserId = ord.user_id;
            orders.OrderTotal = ord.order_total;
            orders.OrderPhone = ord.phone;
            orders.OrderDate = ord.date;
            orders.IsCompleted = ord.is_completed;
            orders.ShippingAddress = ord.shipping_address;
            orders.OrderDescription = ord.description;
            res = _rep.UpdateOrders(orders);
            return res;
        }
        #endregion

        //===========================================================
        //===========================================================

        #region -- Delete Orders --
        public object DeleteOrders(int Id)
        {
            return _rep.DeleteOrders(Id);
        }
        #endregion

        //===========================================================
        //===========================================================

        #region -- Get Order With Pagination --
        public object GetAllOrdersWithPagination(int page, int size)
        {
            var ord = from o in _rep.Context.Orders
                      join u in _rep.Context.Users on o.UserId equals u.UserId
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                       where o.OrderId != null
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                       select new
                       {
                          id = o.OrderId,
                          user_id = o.UserId,
                          username = u.Username,
                          order_total  = o.OrderTotal,
                          phone = o.OrderPhone,
                          date = o.OrderDate,
                          is_completed = o.IsCompleted,
                          shipping_address = o.ShippingAddress,
                          description = o.OrderDescription
                       };
            var offset = (page - 1) * size;
            var total = ord.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = ord.OrderBy(x => x.id).Skip(offset).Take(size).ToList();
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


        #region -- get Revenue With Month --
        public object getRevenueWithMonth(int month, int year)
        {
            return _rep.getRevenueWithMonth(month, year);
        }
        #endregion


        #region -- Get Order By ID -- 
        public object GetOrderById(int id)
        {

            var user = (from o in _rep.Context.Orders
                       join u in _rep.Context.Users on o.UserId equals u.UserId
                       join od in _rep.Context.OrderDetails on o.OrderId equals od.OrderId
                       where o.OrderId == id
                       select new
                       {
                           id = o.OrderId,
                           user_name = u.Username,
                           phone = o.OrderPhone,
                           date = o.OrderDate,
                           is_completed = o.IsCompleted,
                           shipping_address = o.ShippingAddress,
                           description = o.OrderDescription,
                           total = (od.Price * od.Quantity)
                       });

            return user;



        }
        #endregion


    }
}
