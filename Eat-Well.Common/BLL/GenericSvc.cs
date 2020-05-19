using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eat_Well.Common.BLL
{
    using DAL;
    using Rsp;
    public class GenericSvc<D, T> : IGenericSvc<T> where T : class where D : IGenericRep<T>, new()
    {
        #region -- Implements --

        /// <summary>
        /// Create the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public virtual SingleRsp Create(T m)
        {
            var res = new SingleRsp();

            try
            {
                var now = DateTime.Now;
                _rep.Create(m);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }

        /// <summary>
        /// Create the models
        /// </summary>
        /// <param name="l">List model</param>
        /// <returns>Return the result</returns>
        public virtual MultipleRsp Create(List<T> l)
        {
            var res = new MultipleRsp();

            try
            {
                _rep.Create(l);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }

        /// <summary>
        /// Read by
        /// </summary>
        /// <param name="p">Predicate</param>
        /// <returns>Return query data</returns>
        public IQueryable<T> Read(Expression<Func<T, bool>> p)
        {
            return _rep.Read(p);
        }


        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public virtual SingleRsp Read(int id)
        {
            return null;
        }

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>
        public virtual SingleRsp Read(string code)
        {
            return null;
        }

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public virtual SingleRsp Update(T m)
        {
            var res = new SingleRsp();

            try
            {
                _rep.Update(m);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }

        /// <summary>
        /// Update the models
        /// </summary>
        /// <param name="l">List model</param>
        /// <returns>Return the result</returns>
        public virtual MultipleRsp Update(List<T> l)
        {
            var res = new MultipleRsp();

            try
            {
                _rep.Update(l);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }

        /// <summary>
        /// Delete single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRsp Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Delete single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRsp Delete(string code)
        {
            return null;
        }

        /// <summary>
        /// Restore the model
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRsp Restore(int id)
        {
            return null;
        }

        /// <summary>
        /// Restore the model
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRsp Restore(string code)
        {
            return null;
        }

        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public virtual int Remove(int id)
        {
            return 0;
        }

        /// <summary>
        /// Return query all data
        /// </summary>
        public IQueryable<T> All
        {
            get
            {
                return _rep.All;
            }
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public GenericSvc()
        {
            _rep = new D();
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// The repository
        /// </summary>
        protected D _rep;

        #endregion
    }
}