using System;
using System.Collections.Generic;
using System.Text;

namespace ClientAddressManager
{
    /// <summary>
    /// A simple wrapper for returning a result that can be either data or an error message
    /// </summary>
    public class Result<T>
    {
        public ResultCode Code { get; }

        public string Error { get; }

        public T Data { get; }

        public Result(ResultCode code, T data, string error)
        {
            Code = code;
            Data = data;
            Error = error;
        }

    }

    public enum ResultCode
    {
        SUCCESS,
        ERROR
    }

}
