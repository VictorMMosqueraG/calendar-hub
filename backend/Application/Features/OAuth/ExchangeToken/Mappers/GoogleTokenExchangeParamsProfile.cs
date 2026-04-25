namespace Application.Features.OAuth.ExchangeToken.Mappers;

using Application.Features.OAuth.ExchangeToken.Dtos;
using AutoMapper;
using Core.Dtos.AppSettingDto;

public class GoogleTokenExchangeParamsProfile : Profile
{
    public GoogleTokenExchangeParamsProfile()
    {
        CreateMap<OAuthProviderSettingsDto, GoogleTokenExchangeParamsDto>()
            .ForMember(dest => dest.Code, opt => opt.Ignore())
            .ForMember(dest => dest.GrantType, opt => opt.Ignore());
    }
}
