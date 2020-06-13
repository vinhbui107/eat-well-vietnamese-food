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
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
            _svc = new ProductsSvc();
        }

        //Get Method trả về giá trị của 1 Product khi truyền vào 1 ProductId.
        [HttpGet("{Id}")]
        public IActionResult getOrderById(int Id)
        {
            var res = new SingleRsp();
            var order = _svc.GetProductById(Id);
            res.Data = order;
            return Ok(res);
        }

        //Truyền vào 2 tham số page và size.
        //Get method trả về danh sách Product có phân trang.
        [HttpGet]
        public IActionResult GetAllProductWithPagination(int page, int size)
        {
            var res = new SingleRsp();
            var pros = _svc.GetAllProductWithPagination(page, size);
            res.Data = pros;

            return Ok(res);
        }

        //Post Method gửi yêu cầu đến sever.
        //Create Product.
        [HttpPost]
        public IActionResult CreateProduct([FromBody]ProductsReq req)
        {
            var res = _svc.CreateProduct(req);
            return Ok(res);
        }

        //Put, Patch Method cập nhật và ghi đè.
        //Update Product.
        [HttpPut]
        public IActionResult UpdateProduct([FromBody]ProductsReq req)
        {
            var res = _svc.UpdateProduct(req);
            return Ok(res);
        }

        //Detele Method xóa.
        //Delete Product.
        [HttpDelete("{Id}")]
        public IActionResult DeleteProduct(int Id)
        {
            var res = new SingleRsp();
            var del = _svc.DeleteProduct(Id);
            res.Data = del;
            return Ok(res);

        }

        private readonly ProductsSvc _svc;
    }
}