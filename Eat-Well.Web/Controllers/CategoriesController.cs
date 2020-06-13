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

        // Get Method: get category with ID
        [HttpGet("{Id}")]
        public IActionResult getCategoryById(int Id)
        {
            var res = new SingleRsp();
            res = _svc.Read(Id);
            return Ok(res);
        }

        // Get Method: get all categories with pagination
        [HttpGet]
        public IActionResult GetAllCategoryWithPagination(int page, int size)
        {
            var res = new SingleRsp();
            var pros = _svc.GetAllCategoriesWithPagination(page, size);
            res.Data = pros;

            return Ok(res);
        }

        // Post Method: create a new Category
        [HttpPost]
        public IActionResult CreateCategory([FromBody]CategoriesReq req)
        {
            var res = _svc.CreateCategory(req);
            return Ok(res);
        }

        // Put Method: Update a part of Category
        [HttpPut]
        public IActionResult UpdateCategory([FromBody]CategoriesReq req)
        {
            var res = _svc.UpdateCategory(req);
            return Ok(res);
        }

        // Delete Method: Delete all Category
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
