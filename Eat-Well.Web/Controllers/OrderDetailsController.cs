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
    public class OrderDetailsController : ControllerBase
    {

        public OrderDetailsController()
        {
            _svc = new OrderDetailsSvc();
        }

        [HttpGet("GetOrderDetail")]
        public IActionResult GetOrderDetailsById(int ordId, int proId)
        {
            var res = new SingleRsp();
            var ordd = _svc.GetOrderDetailsById(ordId, proId);
            res.Data = ordd;
            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetAllOrderDetailsWithPagination(int page, int size)
        {
            var res = new SingleRsp();
            var ordd = _svc.GetAllOrderDetailsWithPagination(page, size);
            res.Data = ordd;

            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateOrderDetails([FromBody]OrderDetailsReq req)
        {
            var res = _svc.CreateOrderDetails(req);
            return Ok(res);
        }
        [HttpPut("{Id}")]
        public IActionResult UpdateOrderDetails(int Id, [FromBody]OrderDetailsReq req)
        {
            var res = _svc.UpdateOrderDetails(Id, req);
            return Ok(res);
        }

        [HttpDelete]
        public IActionResult DeleteOrderDetails(int ordId, int proId)
        {
            var res = new SingleRsp();
            var del = _svc.DeleteOrderDetails(ordId, proId);
            res.Data = del;
            return Ok(res);

        }



        private readonly OrderDetailsSvc _svc;
    }
}