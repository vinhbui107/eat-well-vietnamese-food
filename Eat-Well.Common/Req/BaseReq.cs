using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Req
{
    public abstract class BaseReq<T>
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public BaseReq()
        {
            Keyword = string.Empty;
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="id">ID</param>
        public BaseReq(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="keyword">Keyword</param>
        public BaseReq(string keyword)
        {
            Keyword = keyword;
        }

        /// <summary>
        /// Convert the request to the model
        /// </summary>
        /// <param name="createdBy">Created by</param>
        /// <returns>Return the result</returns>
        public abstract T ToModel(int? createdBy = null);

        #endregion

        #region -- Properties --

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Keyword
        /// </summary>
        public string Keyword { get; set; }

        #endregion
    }
}