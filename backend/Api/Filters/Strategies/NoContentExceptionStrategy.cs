namespace Api.Filters.Strategies;

using Api.Filters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class NoContentExceptionStrategy : IExceptionHandlerStrategy
{
    public void Handle(ExceptionContext context)
    {
        context.Result = new NoContentResult();
        context.ExceptionHandled = true;
    }
}
