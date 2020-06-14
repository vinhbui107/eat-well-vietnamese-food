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
            orders.IsCompleted = ord.IsCompleted;
            orders.ShippingAddress = ord.ShippingAddress;
            orders.OrderDescription = ord.OrderDescription;
            res = _rep.CreateOrders(orders);
            return res;
        }
        #endregion

        //===========================================================
        //===========================================================
        #region -- Update Orders --

        public SingleRsp UpdateOrders(OrdersReq ord)
        {
            var res = new SingleRsp();
            Orders orders = new Orders();
            orders.OrderId = ord.OrderId;
            orders.UserId = ord.UserId;
            orders.OrderTotal = ord.OrderTotal;
            orders.OrderPhone = ord.OrderPhone;
            orders.OrderDate = ord.OrderDate;
            orders.IsCompleted = ord.IsCompleted;
            orders.ShippingAddress = ord.ShippingAddress;
            orders.OrderDescription = ord.OrderDescription;
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
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            var pro = All.Where(x => x.OrderId != null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
               .Join(_rep.Context.Users, a => a.UserId, b => b.UserId, (a, b) => new
               {
                   a.OrderId,
                   a.UserId,
                   a.OrderTotal,
                   a.OrderPhone,
                   a.OrderDate,
                   a.IsCompleted,
                   a.ShippingAddress,
                   a.OrderDescription,
                   Username = b.Username,
               })
               //.Join(_rep.Context.OrderDetails, a => a.OrderId, b => b.OrderId, (a, b) => new
               //{
               //    a.OrderId,
               //    a.UserId,
               //    a.OrderTotal,
               //    a.OrderPhone,
               //    a.OrderDate,
               //    a.OrderStatus,
               //    a.ShippingAddress,
               //    a.OrderDescription,
               //    a.Username,
               //    Price = b.Price,
               //})
               .OrderBy(x => x.OrderId);

            var offset = (page - 1) * size;
            var total = pro.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = pro.OrderBy(x => x.OrderId).Skip(offset).Take(size).ToList();
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
