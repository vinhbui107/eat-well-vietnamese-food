using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eat_Well.Common.DAL
{
    public interface IGenericRep<T> where T : class
    {
        #region -- Methods --

        /// <summary>
        /// Create the model
        /// </summary>
        /// <param name="m">The model</param>
        void Create(T m);

        /// <summary>
        /// Create list model
        /// </summary>
        /// <param name="l">List model</param>
        void Create(List<T> l);

        /// <summary>
        /// Read by
        /// </summary>
        /// <param name="p">Predicate</param>
        /// <returns>Return query data</returns>
        IQueryable<T> Read(Expression<Func<T, bool>> p);

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        T Read(int id);

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>
        T Read(string code);

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="m">The model</param>
        void Update(T m);

        /// <summary>
        /// Update list model
        /// </summary>
        /// <param name="l">List model</param>
        void Update(List<T> l);

        #endregion

        #region -- Properties --

        /// <summary>
        /// Return query all data
        /// </summary>
        IQueryable<T> All { get; }

        #endregion
    }
}