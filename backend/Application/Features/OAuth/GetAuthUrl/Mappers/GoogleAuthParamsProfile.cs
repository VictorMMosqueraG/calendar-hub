namespace Application.Features.OAuth.GetAuthUrl.Mappers;

using Application.Features.OAuth.GetAuthUrl.Dtos;
using AutoMapper;
using Core.Constants;
using Core.Dtos.AppSettingDto;

public class GoogleAuthParamsProfile : Profile
{
    public GoogleAuthParamsProfile()
    {
        CreateMap<OAuthProviderSettingsDto, GoogleAuthParamsDto>()
            .ForMember(dest => dest.ResponseType, opt => opt.MapFrom(_ => OAuthConstant.ResponseTypeCode))
            .ForMember(dest => dest.AccessType, opt => opt.MapFrom(_ => OAuthConstant.AccessTypeOffline))
            .ForMember(dest => dest.Prompt, opt => opt.MapFrom(_ => OAuthConstant.PromptConsent));
    }
}
