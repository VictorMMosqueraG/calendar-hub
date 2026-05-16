namespace Api.Filters.Strategies;

using Api.Filters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class UnprocessableEntityExceptionStrategy : IExceptionHandlerStrategy
{
    public void Handle(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc4918#section-11.2",
            Title = "Unprocessable Entity",
            Detail = context.Exception.Message
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity
        };
        context.ExceptionHandled = true;
    }
}
