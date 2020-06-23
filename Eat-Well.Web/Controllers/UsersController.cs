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
    public class UsersController : ControllerBase
    {
        public UsersController()
        {
            _svc = new UsersSvc();
        }

        // RESTful API

        // Get Method: api/Users/5
        [HttpGet("{Id}")]
        public IActionResult GetUsersById(int Id)
        {
            var res = new SingleRsp();
            var user = _svc.GetUsersById(Id);
            res.Data = user;
            return Ok(res);
        }

        // Get Method: api/Users
        [HttpGet]
        public IActionResult GetAllUsersWithPagination(int page, int size)
        {
            var res = new SingleRsp();
            var pros = _svc.GetAllUsersWithPagination(page, size);
            res.Data = pros;

            return Ok(res);
        }

        // Post Method: api/Users
        [HttpPost]
        public IActionResult CreateUser([FromBody]UsersReq req)
        {
            var res = _svc.CreateUser(req);
            return Ok(res);
        }

        // Put Method: api/Users/5
        [HttpPut("{Id}")]
        public IActionResult UpdateUser(int Id, [FromBody]UsersReq req)
        {
            var res = _svc.UpdateUser(Id, req);
            return Ok(res);
        }

        // Delete Method: api/Users/5
        [HttpDelete("{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            var res = new SingleRsp();
            var del = _svc.DeleteUser(Id);
            res.Data = del;
            return Ok(res);

        }

        // Authenticate 
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateReq model)
        {
            var user = _svc.AuthencicateUser(model.username, model.password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            // return basic user info and authentication token
            return Ok(new
            {
                id = user.UserId,
            });
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody]UsersReq model)
        {
            var res = _svc.RegisterUser(model);
            return Ok(res);
        }
        private readonly UsersSvc _svc;

        //get order by user 
        [HttpGet("get-order-with-userId/{Id}")]
        public IActionResult getOrderWithUserId(int Id)
        {
            var res = new SingleRsp();
            res.Data = _svc.getOrderWithUserId(Id);
            return Ok(res);
        }
    }
}