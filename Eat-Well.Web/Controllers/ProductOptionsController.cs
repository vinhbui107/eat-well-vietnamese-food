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
    public class ProductOptionsController : ControllerBase
    {
        public ProductOptionsController()
        {
            _svc = new ProductOptionsSvc();

        }

   
        [HttpGet("{Id}")]
        public IActionResult getProductOptionById(int Id)
        {
            var res = new SingleRsp();
            res = _svc.Read(Id);
            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetAllProductOptionsWithPagination(int page, int size)
        {
            var res = new SingleRsp();
            var pros = _svc.GetAllProductOptionsWithPagination(page, size);
            res.Data = pros;

            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateProductOptions([FromBody]ProductOptionsReq req)
        {
            var res = _svc.CreateProductOptions(req);
            return Ok(res);
        }

        [HttpPut]
        public IActionResult UpdateProductOptions([FromBody]ProductOptionsReq req)
        {
            var res = _svc.UpdateProductOptions(req);
            return Ok(res);
        }


        [HttpDelete]
        public IActionResult DeleteProductOptions(int productId, int optionId)
        {
            var res = new SingleRsp();
            var del = _svc.DeleteProductOptions(productId, optionId);
            res.Data = del;
            return Ok(res);

        }



        private readonly ProductOptionsSvc _svc;
    }
}