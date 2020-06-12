using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eat_Well.Web.Controllers
{
    using BLL;
    using DAL.Models;
    using Common.Req;
    using Common.Rsp;

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public OrdersController()
        {
            _svc = new OrdersSvc();
        }
        //Get Method trả về giá trị của 1 Orders khi truyền vào 1 OrdersId.
        [HttpGet("{Id}")]
        public IActionResult getOrderById(int Id)
        {
            var res = new SingleRsp();
            res = _svc.Read(Id);
            return Ok(res);
        }

        //Trả về tất cả Orders
        //Get Method trả về body và head.
        //Head Method trả về head.

        //[HttpGet]
        //public IActionResult getAllOrders()
        //{
        //    var res = new SingleRsp();
        //    res.Data = _svc.All;
        //    return Ok(res);
        //}

        //Truyền vào 2 tham số page và size.
        //Get method trả về danh sách Orders có phân trang.
        [HttpGet("pagination")]
        public IActionResult GetAllOrdersWithPagination(int page, int size)
        {
            var res = new SingleRsp();
            var pros = _svc.GetAllOrdersWithPagination(page, size);
            res.Data = pros;

            return Ok(res);
        }

        //Post Method gửi yêu cầu đến sever.
        //Create Orders.
        [HttpPost]
        public IActionResult CreateOrders([FromBody]OrdersReq req)
        {
            var res = _svc.CreateOrders(req);
            return Ok(res);
        }

        //Put, Patch Method cập nhật và ghi đè.
        //Update Orders.
        [HttpPut]
        public IActionResult UpdateOrders([FromBody]OrdersReq req)
        {
            var res = _svc.UpdateOrders(req);
            return Ok(res);
        }

        //Detele Method xóa.
        //Delete Orders.
        [HttpDelete("{Id}")]
        public IActionResult DeleteOrders(int Id)
        {
            var res = new SingleRsp();
            var del = _svc.DeleteOrders(Id);
            res.Data = del;
            return Ok(res);
        }

        private readonly OrdersSvc _svc;
    }
}