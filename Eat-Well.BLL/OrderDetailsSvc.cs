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
    public class OrderDetailsSvc : GenericSvc<OrderDetailsRep, OrderDetails>
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
        public override SingleRsp Update(OrderDetails m)
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

        public OrderDetailsSvc() { }
        //===========================================================
        //===========================================================
        #region -- Create OrderDetails --

        public SingleRsp CreateOrderDetails(OrderDetailsReq od)
        {
            var res = new SingleRsp();
            OrderDetails orderdetail = new OrderDetails();
            orderdetail.OrderId = od.order_id;
            orderdetail.ProductId = od.product_id;
            orderdetail.Price = od.price;
            orderdetail.Quantity = od.quantity;
            res = _rep.CreateOrderDetails(orderdetail);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Update OrderDetails --

        public SingleRsp UpdateOrderDetails(int id, OrderDetailsReq od)
        {
            var res = new SingleRsp();
            OrderDetails orderdetail = new OrderDetails();
            orderdetail.OrderId = od.order_id;
            orderdetail.ProductId = od.product_id;
            orderdetail.Price = od.price;
            orderdetail.Quantity = od.quantity;
            res = _rep.UpdateOrderDetails(orderdetail);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Delete OrderDetails --
        public object DeleteOrderDetails(int ordId, int proId)
        {
            return _rep.DeleteOrderDetails(ordId, proId);
        }
        #endregion
        //===========================================================
        //===========================================================
        #region -- Get OrderDetails With Pagination --
        public object GetAllOrderDetailsWithPagination(int page, int size)
        {

            var user = from od in _rep.Context.OrderDetails
                       join p in _rep.Context.Products on od.ProductId equals p.ProductId
                       where od.OrderId != null
                       select new
                       {
                           order_id = od.OrderId,
                           product_id = od.ProductId,
                           product_name = p.ProductName,
                           quantity = od.Quantity,
                           price = od.Price,
                           total = (od.Quantity * od.Price)
                       };
            var offset = (page - 1) * size;
            var total = user.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = user.OrderBy(x => x.order_id).Skip(offset).Take(size).ToList();


            var res = new
            {
                data = data,
                tota_record = total,
                total_page = totalpage,
                page = page,
                size = size
            };

            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Get OrderDetails By userID -- 
        public object GetOrderDetailsById(int ordid, int proid)
        {

            var ordd = from od in _rep.Context.OrderDetails
                       join p in _rep.Context.Products on od.ProductId equals p.ProductId
                       join o in _rep.Context.Orders on od.OrderId equals o.OrderId
                       where od.OrderId == ordid && od.ProductId == proid
                       select new
                       {
                           order_id = od.OrderId,
                           product_id = od.ProductId,
                           product_name = p.ProductName,
                           phone = o.OrderPhone,
                           address = o.ShippingAddress,
                           quantity = od.Quantity,
                           price = od.Price,
                           total = (od.Quantity * od.Price)
                       };
            return ordd;
        }
        #endregion
        //===========================================================
        //===========================================================



    }
}
