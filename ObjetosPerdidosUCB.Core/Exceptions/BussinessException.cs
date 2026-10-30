using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ObjetosPerdidosUCB.Core.Exceptions
{
    public class BussinessException : Exception
    {
        public HttpStatusCode StatusCode { get;  }
        public object? Details { get; }
        public BussinessException (string message, 
            HttpStatusCode statusCode = HttpStatusCode.BadRequest, object? details = null)
        {
            StatusCode = statusCode;
            Details = details;
        }
    }
}
