using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BaseAPI.Filter
{
    public class ExceptionFilter:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var cException = context.Exception;
            var exceptionMessage = cException.InnerException == null ? cException.Message : cException.InnerException.Message;
            //We should create self result on it to return front-end
            if (context.Exception is ArgumentException)
            {
                context.Result = new NotFoundObjectResult(cException.Message);
            }
            else
            {
                //It need to be optimized.
                context.Result = new BadRequestObjectResult(exceptionMessage);
            }
            base.OnException(context);
        }
    }
}
