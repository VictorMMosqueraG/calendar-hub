namespace Application.Features.OAuth.GetAuthUrl.Mappers;

using Application.Features.OAuth.GetAuthUrl.Dtos;
using AutoMapper;
using Core.Dtos.AppSettingDto;

public class GoogleAuthParamsProfile : Profile
{
    public GoogleAuthParamsProfile()
    {
        CreateMap<OAuthProviderSettingsDto, GoogleAuthParamsDto>();
    }
}
