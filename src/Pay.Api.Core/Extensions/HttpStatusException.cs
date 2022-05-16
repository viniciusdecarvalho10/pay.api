using System;
using System.Net;

namespace Pay.Api.Core.Extensions
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public HttpStatusException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }
        public HttpStatusException(HttpStatusCode statusCode, string message, string errorCode) : base(message)
        {
            this.StatusCode = statusCode;
            this.ErrorCode = errorCode;
        }
    }
}