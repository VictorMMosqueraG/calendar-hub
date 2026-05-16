namespace Api.Filters.Strategies;

using Api.Filters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class NotFoundExceptionStrategy : IExceptionHandlerStrategy
{
    public void Handle(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = context.Exception.Message
        };

        context.Result = new NotFoundObjectResult(details);
        context.ExceptionHandled = true;
    }
}
