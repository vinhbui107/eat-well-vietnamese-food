using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Rsp
{
    public class SingleRsp : BaseRsp
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public SingleRsp() : base() { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="message">Message</param>
        public SingleRsp(string message) : base(message) { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="titleError">Title error</param>
        public SingleRsp(string message, string titleError) : base(message, titleError) { }

        /// <summary>
        /// Set data
        /// </summary>
        /// <param name="code">Success code</param>
        /// <param name="data">Data</param>
        public void SetData(string code, object data)
        {
            Code = code;
            Data = data;
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Data
        /// </summary>
        public object Data { get; set; }

        #endregion
    }
}