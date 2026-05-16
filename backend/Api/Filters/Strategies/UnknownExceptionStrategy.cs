namespace Api.Filters.Strategies;

using Api.Filters.Interfaces;
using Core.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class UnknownExceptionStrategy : IExceptionHandlerStrategy
{
    public void Handle(ExceptionContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
            return;
        }

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = Message.InternalServerError,
            Detail = $"{context.Exception.Message}"
        };

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}
