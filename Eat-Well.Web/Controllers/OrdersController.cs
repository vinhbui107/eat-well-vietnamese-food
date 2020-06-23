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

        // RESTful API

        // Get Method: api/Orders/5
        [HttpGet("{Id}")]
        public IActionResult getOrdersById(int Id)
        {
            var res = new SingleRsp();
            var cate = _svc.GetOrderById(Id);
            res.Data = cate;
            return Ok(res);
        }

        // Get Method: api/Orders/
        [HttpGet]
        public IActionResult GetAllOrdersWithPagination(int page, int size)
        {
            var res = new SingleRsp();
            var pros = _svc.GetAllOrdersWithPagination(page, size);
            res.Data = pros;

            return Ok(res);
        }

        // Post Method: api/Orders/
        [HttpPost]
        public IActionResult CreateOrders([FromBody]OrdersReq req)
        {
            var res = _svc.CreateOrders(req);
            return Ok(res);
        }

        // Put Method: api/Orders/5
        [HttpPut("{Id}")]
        public IActionResult UpdateOrders(int Id, [FromBody]OrdersReq req)
        {
            var res = _svc.UpdateOrders(Id, req);
            return Ok(res);
        }

        // Delete Method: api/Orders/5
        [HttpDelete("{Id}")]
        public IActionResult DeleteOrders(int Id)
        {
            var res = new SingleRsp();
            var del = _svc.DeleteOrders(Id);
            res.Data = del;
            return Ok(res);
        }

        //get revenue 
        [HttpGet("get-revenue-with-month")]
        public IActionResult getRevenueWithMonth(int month, int year)
        {
            var res = new SingleRsp();
            res.Data = _svc.getRevenueWithMonth(month, year);
            return Ok(res);
        }

        private readonly OrdersSvc _svc;



    }
}