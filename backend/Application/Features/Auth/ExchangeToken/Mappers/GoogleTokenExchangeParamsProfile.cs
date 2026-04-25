namespace Application.Features.Auth.ExchangeToken.Mappers;

using Application.Features.Auth.ExchangeToken.Dtos;
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
