namespace Application.Features.Auth.Disconnect.Commands;

using Core.Dtos.ResponsesDto;
using MediatR;

public record DisconnectCommand : IRequest<ResultDto>
{
    public required string Provider { get; init; }
}
