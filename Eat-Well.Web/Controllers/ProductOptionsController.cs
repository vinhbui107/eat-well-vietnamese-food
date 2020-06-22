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

        [HttpPut("{Id}")]
        public IActionResult UpdateProductOptions(int Id, [FromBody]ProductOptionsReq req)
        {
            var res = _svc.UpdateProductOptions(Id, req);
            return Ok(res);
        }


    

        private readonly ProductOptionsSvc _svc;
    }
}