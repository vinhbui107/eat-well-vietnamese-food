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

        [HttpGet("{Id}")]
        public IActionResult getCategoryById(int Id)
        {
            var res = new SingleRsp();
            res = _svc.Read(Id);
            return Ok(res);
        }

        [HttpGet]
        public IActionResult getAllCategories()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        [HttpGet("pagination")]
        public IActionResult GetAllProductWithPagination(int page, int size)
        {
            EatWellDBContext db = new EatWellDBContext();
            var cate = db.Categories.ToList();
            var offset = (page - 1) * size;
            var total = cate.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = cate.OrderBy(x => x.CategoryId).Skip(offset).Take(size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalpage,
                Page = page,
                Size = size
            };
            
            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody]CategoriesReq req)
        {
            var res = _svc.CreateCategory(req);
            return Ok(res);
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody]CategoriesReq req)
        {
            var res = _svc.UpdateCategory(req);
            return Ok(res);
        }

        [HttpDelete]
        public bool RemoveCategory(int id)
        {
            EatWellDBContext db = new EatWellDBContext();
            Categories category = db.Categories.FirstOrDefault(x => x.CategoryId == id);
            
            if (category == null) return false;
            
            db.Categories.Remove(category);
            db.SaveChanges();
            
            return true;
        }

        private readonly CategoriesSvc _svc;
    }
}