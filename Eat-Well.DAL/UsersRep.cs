using Eat_Well.Common.DAL;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eat_Well.DAL
{
    public class UsersRep : GenericRep<EatWellDBContext, Users>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Users Read(int id)
        {
            var res = All.FirstOrDefault(p => p.UserId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.UserId == id);
            m = base.Delete(m); //TODO
            return m.UserId;
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>

        #endregion


        //=================================================================
        //=================================================================
        //=================================================================
        #region -- Create User --
        public SingleRsp CreateUser(Users user)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Users.Add(user);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        #endregion
        //=================================================================
        //=================================================================
        //=================================================================
        #region -- Update User --
        public SingleRsp UpdateUser(Users user)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Users.Update(user);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        #endregion
        //=================================================================
        //=================================================================
        //=================================================================
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
        //=================================================================
        //=================================================================
        //=================================================================
    }
}