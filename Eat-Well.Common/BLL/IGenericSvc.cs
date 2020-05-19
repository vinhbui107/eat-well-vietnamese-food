using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eat_Well.Common.BLL
{
    using Rsp;
    public interface IGenericSvc<T> where T : class
    {
        #region -- Methods --

        /// <summary>
        /// Create the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        SingleRsp Create(T m);

        /// <summary>
        /// Create the models
        /// </summary>
        /// <param name="l">List model</param>
        /// <returns>Return the result</returns>
        MultipleRsp Create(List<T> l);

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
        SingleRsp Read(int id);

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>
        SingleRsp Read(string code);

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        SingleRsp Update(T m);

        /// <summary>
        /// Update the models
        /// </summary>
        /// <param name="l">List model</param>
        /// <returns>Return the result</returns>
        MultipleRsp Update(List<T> l);

        /// <summary>
        /// Delete single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the result</returns>
        SingleRsp Delete(int id);

        /// <summary>
        /// Delete single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the result</returns>
        SingleRsp Delete(string code);

        /// <summary>
        /// Restore the model
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the result</returns>
        SingleRsp Restore(int id);

        /// <summary>
        /// Restore the model
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the result</returns>
        SingleRsp Restore(string code);

        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        int Remove(int id);

        #endregion

        #region -- Properties --

        /// <summary>
        /// Return query all data
        /// </summary>
        IQueryable<T> All { get; }

        #endregion
    }
}