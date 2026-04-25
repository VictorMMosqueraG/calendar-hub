namespace Application.Features.Auth.ExchangeToken.Commands;

using Core.Dtos.ResponsesDto;
using Core.Messages;
using Application.Interfaces.Services;
using Application.Interfaces.Wrappers;
using MediatR;

public class ExchangeTokenCommandHandler(
    IOAuthWrapper oAuthWrapper,
    ITokenStore tokenStore
) : IRequestHandler<ExchangeTokenCommand, ResultDto>
{
    private readonly IOAuthWrapper _oAuthWrapper = oAuthWrapper;
    private readonly ITokenStore _tokenStore = tokenStore;

    public async Task<ResultDto> Handle(
        ExchangeTokenCommand request,
        CancellationToken cancellationToken)
    {
        var accessToken = await _oAuthWrapper
            .ExchangeCodeForTokenAsync(request.Provider, request.Code, cancellationToken);
            
        _tokenStore.SetToken(request.Provider, accessToken);
        
        return ResultDto.Success(Message.EntityCreateSuccess("Token"));
    }
}
