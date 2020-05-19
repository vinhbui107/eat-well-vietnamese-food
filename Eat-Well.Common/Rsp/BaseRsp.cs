using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Rsp
{
    public class BaseRsp
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public BaseRsp()
        {
            Success = true;
            msg = string.Empty;
            titleError = "Error";

            Dev = true; // TODO

            if (string.IsNullOrEmpty(err))
            {
                err = "Please update common error in Custom Settings";
            }
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="message">Message</param>
        public BaseRsp(string message) : this()
        {
            msg = message;
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="titleError">Title error</param>
        public BaseRsp(string message, string titleError) : this(message)
        {
            this.titleError = titleError;
        }

        /// <summary>
        /// Set error
        /// </summary>
        /// <param name="message">Message</param>
        public void SetError(string message)
        {
            Success = false;
            msg = message;
        }

        /// <summary>
        /// Set error
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Message</param>
        public void SetError(string code, string message)
        {
            Success = false;
            Code = code;
            msg = message;
        }

        /// <summary>
        /// Set message
        /// </summary>
        /// <param name="message">Message</param>
        public void SetMessage(string message)
        {
            msg = message;
        }

        /// <summary>
        /// Test error
        /// </summary>
        public void TestError()
        {
            SetError("We are testing to show error message, please ignore it...");
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Error or success code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message
        {
            get
            {
                if (Success)
                {
                    return msg;
                }
                else
                {
                    return Dev ? msg : err;
                }
            }
        }

        /// <summary>
        /// Variant
        /// </summary>
        public string Variant
        {
            get
            {
                return Success ? "success" : "error";
            }
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get
            {
                return Success ? "Success" : titleError;
            }
        }

        /// <summary>
        /// Developer mode
        /// </summary>
        public static bool Dev { get; set; }

        #endregion

        #region -- Fields --

        /// <summary>
        /// Error
        /// </summary>
        private readonly string err;

        /// <summary>
        /// Title error
        /// </summary>
        private readonly string titleError;

        /// <summary>
        /// Message
        /// </summary>
        private string msg;

        #endregion
    }
}