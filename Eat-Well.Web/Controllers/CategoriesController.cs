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
    public class CategoriesController : ControllerBase
    {
        public CategoriesController()
        {
            _svc = new CategoriesSvc();
        }
        
        // RESTful API

        // Get method: api/Categories/5
        [HttpGet("{Id}")]
       
        public IActionResult getCategoryById(int Id)
        {
            var res = new SingleRsp();
            var cate = _svc.GetCategoryById(Id);
            res.Data = cate;
            return Ok(res);
        }

        // Get method: api/Categories
        [HttpGet]
        public IActionResult GetAllCategoriesWithPagination(int page, int size)
        {
            var res = new SingleRsp();
            var pros = _svc.GetAllCategoriesWithPagination(page, size);
            res.Data = pros;

            return Ok(res);
        }

        // Post method: api/Categories
        [HttpPost]
        public IActionResult CreateCategory([FromBody]CategoriesReq req)
        {
            var res = _svc.CreateCategory(req);
            return Ok(res);
        }

        // Put method: api/Categories/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id,[FromBody]CategoriesReq req)
        {
            var res = _svc.UpdateCategory(id, req);
            return Ok(res);
        }

        // Delete method: api/Categories/5
        [HttpDelete("{Id}")]
        public IActionResult DeleteCategory(int Id)
        {
            var res = new SingleRsp();
            var del = _svc.DeleteCategory(Id);
            res.Data = del;
            return Ok(res);

        }
        private readonly CategoriesSvc _svc;
    }
}
