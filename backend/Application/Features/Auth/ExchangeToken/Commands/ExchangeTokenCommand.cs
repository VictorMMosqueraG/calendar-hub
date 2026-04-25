namespace Application.Features.Auth.ExchangeToken.Commands;

using Core.Dtos.ResponsesDto;
using MediatR;

public record ExchangeTokenCommand : IRequest<ResultDto>
{
    public required string Provider { get; init; }
    public required string Code { get; init; }
}
