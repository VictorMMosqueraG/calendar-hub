namespace Api.Filters.Strategies;

using Api.Filters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ConflictExceptionStrategy : IExceptionHandlerStrategy
{
    public void Handle(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Title = "Conflict",
            Detail = context.Exception.Message
        };

        context.Result = new ConflictObjectResult(details);
        context.ExceptionHandled = true;
    }
}
