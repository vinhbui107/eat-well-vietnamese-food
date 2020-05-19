using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eat_Well.Common.DAL
{
    public class GenericRep<C, T> : IGenericRep<T> where T : class where C : DbContext, new()
    {
        #region -- Implements --

        /// <summary>
        /// Create the model
        /// </summary>
        /// <param name="m">The model</param>
        public void Create(T m)
        {
            _context.Set<T>().Add(m);
            _context.SaveChanges();
        }

        /// <summary>
        /// Create list model
        /// </summary>
        /// <param name="l">List model</param>
        public void Create(List<T> l)
        {
            _context.Set<T>().AddRange(l);
            _context.SaveChanges();
        }

        /// <summary>
        /// Read by
        /// </summary>
        /// <param name="p">Predicate</param>
        /// <returns>Return query data</returns>
        public IQueryable<T> Read(Expression<Func<T, bool>> p)
        {
            return _context.Set<T>().Where(p);
        }

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public virtual T Read(int id)
        {
            return null;
        }

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>
        public virtual T Read(string code)
        {
            return null;
        }

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="m">The model</param>
        public void Update(T m)
        {
            _context.Set<T>().Update(m);
            _context.SaveChanges();
        }

        /// <summary>
        /// Update list model
        /// </summary>
        /// <param name="l">List model</param>
        public void Update(List<T> l)
        {
            _context.Set<T>().UpdateRange(l);
            _context.SaveChanges();
        }

        /// <summary>
        /// Return query all data
        /// </summary>
        public IQueryable<T> All
        {
            get
            {
                return _context.Set<T>();
            }
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public GenericRep()
        {
            _context = new C();
        }

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="old">The old model</param>
        /// <param name="new">The new model</param>
        protected object Update(T old, T @new)
        {
            _context.Entry(old).State = EntityState.Modified;
            var res = _context.Set<T>().Add(@new);
            return res;
        }

        /// <summary>
        /// Delete the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the object</returns>
        protected T Delete(T m)
        {
            var t = _context.Set<T>().Remove(m);
            return t.Entity;
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// The database context
        /// </summary>
        public C Context
        {
            get { return _context; }
            set { _context = value; }
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// The entities
        /// </summary>
        private C _context;

        #endregion
    }
}