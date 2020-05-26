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
        [HttpGet, HttpHead]
        public IActionResult getAllOrder()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        //Truyền vào 2 tham số page và size.
        //Get method trả về danh sách Orders có phân trang.
        [HttpGet("pagination")]
        public IActionResult GetAllOrderWithPagination(int page, int size)
        {
            EatWellDBContext db = new EatWellDBContext();
            var pro = db.Orders.ToList();
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
            }; ;
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

        //Put Method cập nhật và ghi đè.
        //Update Orders.
        [HttpPut]
        public IActionResult UpdateOrders([FromBody]OrdersReq req)
        {
            var res = _svc.UpdateOrders(req);
            return Ok(res);
        }

        //Detele Method xóa.
        //Delete Orders.
        [HttpDelete]
        public bool RemoveOrders(int id)
        {
            EatWellDBContext db = new EatWellDBContext();
            //lấy orders tồn tại ra
            Orders order = db.Orders.FirstOrDefault(x => x.OrderId == id);
            if (order == null) return false;
            //xóa product
            db.Orders.Remove(order);
            // lưu thay đổi 
            db.SaveChanges();
            return true;
        }
        private readonly OrdersSvc _svc;
    }
}