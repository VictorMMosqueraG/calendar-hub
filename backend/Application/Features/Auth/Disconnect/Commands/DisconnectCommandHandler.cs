namespace Application.Features.Auth.Disconnect.Commands;

using Application.Interfaces.Services;
using Core.Dtos.ResponsesDto;
using Core.Messages;
using MediatR;

public class DisconnectCommandHandler(
    ITokenStore tokenStore
) : IRequestHandler<DisconnectCommand, ResultDto>
{
    private readonly ITokenStore _tokenStore = tokenStore;

    public async Task<ResultDto> Handle(
        DisconnectCommand request,
        CancellationToken cancellationToken)
    {
        _tokenStore.RemoveToken(request.Provider);

        return ResultDto.Success(Message.EntityDeleteSuccess(request.Provider));
    }
}
