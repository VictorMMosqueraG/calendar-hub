namespace Api.Filters.Interfaces;

using Microsoft.AspNetCore.Mvc.Filters;

public interface IExceptionHandlerStrategy
{
    void Handle(ExceptionContext context);
}
