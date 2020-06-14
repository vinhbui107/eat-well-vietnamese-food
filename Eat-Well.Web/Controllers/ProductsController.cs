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

        // RESTful API

        //Get Method: api/Products/5
        [HttpGet("{Id}")]
        public IActionResult GetProductById(int Id)
        {
            var res = new SingleRsp();
            var order = _svc.GetProductById(Id);
            res.Data = order;
            return Ok(res);
        }

        // Get Method: api/Products
        [HttpGet]
        public IActionResult GetAllProductWithPagination(int page, int size)
        {
            var res = new SingleRsp();
            var pros = _svc.GetAllProductWithPagination(page, size);
            res.Data = pros;

            return Ok(res);
        }

        // Post Method: api/Products
        [HttpPost]
        public IActionResult CreateProduct([FromBody]ProductsReq req)
        {
            var res = _svc.CreateProduct(req);
            return Ok(res);
        }

        // Put Method: api/Products/5
        [HttpPut]
        public IActionResult UpdateProduct([FromBody]ProductsReq req)
        {
            var res = _svc.UpdateProduct(req);
            return Ok(res);
        }

        // Delete Method: api/Products/5
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