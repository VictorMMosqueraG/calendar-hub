namespace Application.Features.OAuth.ExchangeToken.Commands;

using Core.Dtos.ResponsesDto;
using MediatR;

public record ExchangeTokenCommand : IRequest<ResultDto>
{
    public string? Provider { get; init; }
    public string? Code { get; init; }
}
