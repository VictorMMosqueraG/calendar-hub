namespace Application.Features.Auth.GetAuthUrl.Mappers;

using Application.Features.Auth.GetAuthUrl.Dtos;
using AutoMapper;
using Core.Dtos.AppSettingDto;

public class GoogleAuthParamsProfile : Profile
{
    public GoogleAuthParamsProfile()
    {
        CreateMap<OAuthProviderSettingsDto, GoogleAuthParamsDto>();
    }
}
