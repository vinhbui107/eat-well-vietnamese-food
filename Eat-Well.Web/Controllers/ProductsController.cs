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
        public IActionResult getProductById(int Id)
        {
            var res = new SingleRsp();
            res = _svc.Read(Id);
            return Ok(res);
        }

        //Trả về tất cả Product
        //Get Method trả về body và head.
        //Head Method trả về head.
        [HttpGet, HttpHead]
        public IActionResult getAllProducts()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        //Truyền vào 2 tham số page và size.
        //Get method trả về danh sách Product có phân trang.
        [HttpGet("pagination")]
        public IActionResult GetAllProductWithPagination(int page, int size)
        {
            EatWellDBContext db = new EatWellDBContext();
            var pro = db.Products.ToList();
            var offset = (page - 1) * size;
            var total = pro.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = pro.OrderBy(x => x.ProductId).Skip(offset).Take(size).ToList();
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
        //Create Product.
        [HttpPost]
        public IActionResult CreateProduct([FromBody]ProductsReq req)
        {
            var res = _svc.CreateProduct(req);
            return Ok(res);
        }

        //Put Method cập nhật và ghi đè.
        //Update Product.
        [HttpPut]
        public IActionResult UpdateProduct([FromBody]ProductsReq req)
        {
            var res = _svc.UpdateProduct(req);
            return Ok(res);
        }

        //Detele Method xóa.
        //Delete Product.
        [HttpDelete]
        public bool RemoveProduct(int id)
        {
            EatWellDBContext db = new EatWellDBContext();
            //lấy product tồn tại ra
            Products product = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null) return false;
            //xóa product
            db.Products.Remove(product);
            // lưu thay đổi 
            db.SaveChanges(); 
            return true;
        }

        //[HttpDelete]
        //public IActionResult RemoveProduct([FromBody]ProductsReq req)
        //{
        //    var res = _svc.RemoveProduct(req);
        //    return Ok(res);
        //}

    


        private readonly ProductsSvc _svc;
    }
}