namespace Application.Features.OAuth.ExchangeToken.Commands;

using Application.Features.OAuth.ExchangeToken.Services;
using Application.Interfaces.Services;
using Core.Dtos.ResponsesDto;
using Core.Messages;
using MediatR;

public class ExchangeTokenCommandHandler(
    TokenExchangeService tokenExchangeService,
    ITokenStore tokenStore
) : IRequestHandler<ExchangeTokenCommand, ResultDto>
{
    private readonly TokenExchangeService _tokenExchangeService = tokenExchangeService;
    private readonly ITokenStore _tokenStore = tokenStore;

    public async Task<ResultDto> Handle(
        ExchangeTokenCommand request,
        CancellationToken cancellationToken)
    {
        var accessToken = await _tokenExchangeService.ExchangeAsync(
            request.Provider, request.Code, cancellationToken);

        _tokenStore.SetToken(request.Provider, accessToken);

        return ResultDto.Success(Message.EntityCreateSuccess("Token"));
    }
}
