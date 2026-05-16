namespace Application.Features.OAuth.GetAuthUrl.Builders;

using Application.Features.OAuth.GetAuthUrl.Dtos;
using Application.Features.OAuth.GetAuthUrl.Extensions;
using Application.Features.OAuth.GetAuthUrl.Interfaces;
using Application.Features.OAuth.Interfaces;
using AutoMapper;

public class AuthUrlBuilder(
    IOAuthProviderResolver providerResolver,
    IMapper mapper
) : IAuthUrlBuilder
{
    private readonly IOAuthProviderResolver _providerResolver = providerResolver;
    private readonly IMapper _mapper = mapper;

    public GetAuthUrlResponseDto BuildAuthUrl(string providerName)
    {
        var provider = _providerResolver.ResolveByName(providerName);
        var authParams = _mapper.Map<GoogleAuthParamsDto>(provider);

        return new GetAuthUrlResponseDto { Url = authParams.ToUrl(provider.AuthUrl ?? string.Empty) };
    }
}
