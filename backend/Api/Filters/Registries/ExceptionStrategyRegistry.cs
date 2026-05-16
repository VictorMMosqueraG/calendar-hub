namespace Api.Filters.Registries;

using Api.Filters.Interfaces;
using Api.Filters.Strategies;
using Core.Exceptions;

public static class ExceptionStrategyRegistry
{
    private static readonly Dictionary<Type, IExceptionHandlerStrategy> Strategies = new()
    {
        { typeof(ValidationException), new ValidationExceptionStrategy() },
        { typeof(NotFoundException), new NotFoundExceptionStrategy() },
        { typeof(NoContentException), new NoContentExceptionStrategy() },
        { typeof(BadRequestException), new BadRequestExceptionStrategy() },
        { typeof(ConflictException), new ConflictExceptionStrategy() },
        { typeof(UnprocessableEntityException), new UnprocessableEntityExceptionStrategy() },
    };

    private static readonly IExceptionHandlerStrategy DefaultStrategy = new UnknownExceptionStrategy();

    public static IExceptionHandlerStrategy Resolve(Type exceptionType)
    {
        return Strategies.GetValueOrDefault(exceptionType, DefaultStrategy);
    }
}
