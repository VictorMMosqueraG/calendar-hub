namespace Api.Filters;

using Api.Filters.Registries;
using Microsoft.AspNetCore.Mvc.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var strategy = ExceptionStrategyRegistry.Resolve(context.Exception.GetType());
        strategy.Handle(context);

        base.OnException(context);
    }
}
