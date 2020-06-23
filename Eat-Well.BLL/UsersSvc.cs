using Eat_Well.Common.BLL;
using Eat_Well.Common.Req;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL;
using Eat_Well.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Eat_Well.BLL
{
    public class UsersSvc: GenericSvc<UsersRep, Users>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public override SingleRsp Update(Users m)
        {
            var res = new SingleRsp();

            var m1 = m.UserId > 0 ? _rep.Read(m.UserId) : _rep.Read(m.Username);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }

            return res;
        }
        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public UsersSvc() { }

        #endregion
        //===========================================================
        //===========================================================
        
        #region -- Create Users --

        public SingleRsp CreateUser(UsersReq u)
        {
            var res = new SingleRsp();
            Users user = new Users();
            user.UserId = u.id;
            user.Username = u.username;
            user.Password = u.password;
            user.Email = u.email;
            user.FullName = u.full_name;
            user.Phone = u.phone;
            user.Address = u.address;
            user.IsAdmin = u.is_admin;
            user.IsActive = u.is_active;
            res = _rep.CreateUser(user);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Update User --

        public SingleRsp UpdateUser(int id, UsersReq u)
        {
            var res = new SingleRsp();
            var user = All.FirstOrDefault(x => x.UserId.Equals(id));
            user.Username = u.username;
            user.Password = u.password;
            user.Email = u.email;
            user.FullName = u.full_name;
            user.Phone = u.phone;
            user.Address = u.address;
            user.IsAdmin = u.is_admin;
            user.IsActive = u.is_active;
            res = _rep.UpdateUser(user);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Delete User --
        public object DeleteUser(int Id)
        {
            return _rep.DeleteUser(Id);
        }
        #endregion

        //===========================================================
        //===========================================================

        #region -- Get Users With Pagination --
        public object GetAllUsersWithPagination(int page, int size)
        {

            var user = from u in _rep.Context.Users
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                       where u.UserId != null
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                       select new
                       {
                           id = u.UserId,
                           username = u.Username,
                           email = u.Email,
                           full_name = u.FullName,
                           password = u.Password,
                           address = u.Address,
                           phone = u.Phone,
                           is_admin = u.IsAdmin,
                           is_active = u.IsActive

                       };
            var offset = (page - 1) * size;
            var total = user.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = user.OrderBy(x => x.id).Skip(offset).Take(size).ToList();


            var res = new
            {
                data = data,
                tota_record = total,
                total_page = totalpage,
                page = page,
                size = size
            };

            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Get Users By ID -- 
        public object GetUsersById(int id)
        {
           
            var user = from u in _rep.Context.Users
                        where  u.UserId == id
                        select new 
                        {
                            id = u.UserId,
                            username = u.Username,
                            email = u.Email,
                            full_name = u.FullName,
                            password = u.Password,
                            address = u.Address,
                            phone = u.Phone,
                            is_admin = u.IsAdmin,
                            is_active = u.IsActive

                        };
            return user;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Authenticate --
        public Users AuthencicateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _rep.Context.Users.Where(x => x.Username == username).FirstOrDefault();

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (user.Password != password)
                return null;

            // authentication successful
            return user;

        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Authenticate --
        public SingleRsp RegisterUser(UsersReq u)
        {
            var res = new SingleRsp();
            Users user = new Users();
            user.UserId = u.id;
            user.Username = u.username;
            user.Password = u.password;
            user.Email = u.email;
            user.FullName = u.full_name;
            user.Phone = u.phone;
            user.Address = u.address;
            user.IsAdmin = false;
            user.IsActive = u.is_active;
            res = _rep.CreateUser(user);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- get Order With UserId --
        public object getOrderWithUserId(int userid)
        {
            return _rep.getOrderWithUserId(userid);
        }
        #endregion

    }
}
