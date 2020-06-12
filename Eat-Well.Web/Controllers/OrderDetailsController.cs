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

        [HttpGet("{Id}")]
        public IActionResult getOrderDetailsById(int Id)
        {
            var res = new SingleRsp();
            res = _svc.Read(Id);
            return Ok(res);
        }

        //Trả về tất cả OrderDetails
        //Get Method trả về body và head.
        //Head Method trả về head.
        [HttpGet]
        public IActionResult getAllOrderDetails()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        //Truyền vào 2 tham số page và size.
        //Get method trả về danh sách Order có phân trang.
        [HttpGet("pagination")]
        public IActionResult GetAllOrderDetailsWithPagination(int page, int size)
        {
            var res = new SingleRsp();
            var pros = _svc.GetAllOrderDetailsWithPagination(page, size);
            res.Data = pros;

            return Ok(res);
        }

        //Post Method gửi yêu cầu đến sever.
        //Create Order.
        [HttpPost]
        public IActionResult CreateOrdersDetails([FromBody]OrderDetailsReq req)
        {
            var res = _svc.CreateOrdersDetails(req);
            return Ok(res);
        }

        //Put, Patch Method cập nhật và ghi đè.
        //Update ORder.
        [HttpPut]
        public IActionResult UpdateOrderDetails([FromBody]OrderDetailsReq req)
        {
            var res = _svc.UpdateOrderDetails(req);
            return Ok(res);
        }

        //Detele Method xóa.
        //Delete Order.
        [HttpDelete]
        public IActionResult DeleteOrderDetails(int OrdId, int ProId)
        {
            var res = new SingleRsp();
            var del = _svc.DeleteOrderDetails(OrdId,ProId);
            res.Data = del;
            return Ok(res);

        }

        private readonly OrderDetailsSvc _svc;
    }
}