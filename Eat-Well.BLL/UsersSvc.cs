using Eat_Well.Common.BLL;
using Eat_Well.Common.Req;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL;
using Eat_Well.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            user.UserId = u.UserId;
            user.Username = u.Username;
            user.Password = u.Password;
            user.Email = u.Email;
            user.FullName = u.FullName;
            user.Phone = u.Phone;
            user.Address = u.Address;
            user.IsAdmin = u.IsAdmin;
            user.IsActive = u.IsActive;
            res = _rep.CreateUser(user);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================
        #region -- Update User --

        public SingleRsp UpdateUser(UsersReq u)
        {
            var res = new SingleRsp();
            Users user = new Users();
            user.UserId = u.UserId;
            user.Username = u.Username;
            user.Password = u.Password;
            user.Email = u.Email;
            user.FullName = u.FullName;
            user.Phone = u.Phone;
            user.Address = u.Address;
            user.IsAdmin = u.IsAdmin;
            user.IsActive = u.IsActive;
            res = _rep.UpdateUser(user);
            return res;
        }
        #endregion
        //===========================================================
        //===========================================================

        #region -- Delete User --
        public bool DeleteUser(int Id)
        {
            EatWellDBContext db = new EatWellDBContext();
            Users user = db.Users.FirstOrDefault(x => x.UserId == Id);
            if (user == null) return false;
            db.Users.Remove(user);
            db.SaveChangesAsync();
            return true;
        }
        #endregion

        //===========================================================
        //===========================================================

        #region -- Get Users With Pagination --
        public object GetAllUsersWithPagination(int page, int size)
        {
            EatWellDBContext db = new EatWellDBContext();
            var user = db.Users.ToList();
            var offset = (page - 1) * size;
            var total = user.Count();
            int totalpage = (total % size) == 0 ? (total / size) : (int)((total / size) + 1);
            var data = user.OrderBy(x => x.UserId).Skip(offset).Take(size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalpage,
                Page = page,
                Size = size
            };

            return res;
        }
        #endregion
        //===========================================================
        //===========================================================
    }
}
