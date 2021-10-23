using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models.Responses
{
    public class GenericResult<T>
    {
        public HttpStatusCode StatusCode { get; }
        public List<FieldError> FieldErrorMessages { get; set; }
        public string AdditionalErrorMessage { get; }
        public T Data { get; }

        public GenericResult(HttpStatusCode statusCode, T data, List<FieldError> fieldErrorMessages = null, string additionalErrorMessage = null)
        {
            StatusCode = statusCode;
            FieldErrorMessages = fieldErrorMessages;
            AdditionalErrorMessage = additionalErrorMessage;
            Data = data;
        }
    }

    public class FieldError
    {
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
