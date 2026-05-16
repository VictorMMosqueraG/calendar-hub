namespace Application.Features.OAuth.ExchangeToken.Services;

using Application.Features.OAuth.ExchangeToken.Dtos;
using Application.Features.OAuth.ExchangeToken.Extensions;
using Application.Features.OAuth.Interfaces;
using Application.Interfaces.Wrappers;
using AutoMapper;

public class TokenExchangeService(
    IOAuthWrapper oAuthWrapper,
    IOAuthProviderResolver providerResolver,
    IMapper mapper
)
{
    private readonly IOAuthWrapper _oAuthWrapper = oAuthWrapper;
    private readonly IOAuthProviderResolver _providerResolver = providerResolver;
    private readonly IMapper _mapper = mapper;

    public async Task<string> ExchangeCodeForTokenAsync(
        string providerName,
        string code,
        CancellationToken cancellationToken)
    {
        var provider = _providerResolver.ResolveByName(providerName);
        var exchangeParams = _mapper.Map<GoogleTokenExchangeParamsDto>(provider) with { Code = code };

        return await _oAuthWrapper.ExchangeCodeForTokenAsync(
            provider.TokenUrl ?? string.Empty, exchangeParams.ToDictionary(), cancellationToken);
    }
}
