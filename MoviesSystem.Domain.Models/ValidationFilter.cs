using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviesSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(keyValue => keyValue.Key, keyValue => keyValue.Value.Errors.Select(error => error.ErrorMessage)).ToArray();

                var errors = new List<FieldError>();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        FieldError errorModel = new FieldError
                        {
                            FieldName = error.Key,
                            ErrorMessage = subError,
                        };
                        errors.Add(errorModel);
                    }
                }
                GenericResult<object> result = new GenericResult<object>(HttpStatusCode.BadRequest, errors, null, null);
                context.Result = new ObjectResult(result) { StatusCode = (int)result.StatusCode };
            }
        }
    }
}
