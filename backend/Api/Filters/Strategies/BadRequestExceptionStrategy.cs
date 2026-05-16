namespace Api.Filters.Strategies;

using Api.Filters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BadRequestExceptionStrategy : IExceptionHandlerStrategy
{
    public void Handle(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = context.Exception.Message
        };

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }
}
