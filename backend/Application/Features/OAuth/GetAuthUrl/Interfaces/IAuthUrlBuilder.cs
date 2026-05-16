namespace Application.Features.OAuth.GetAuthUrl.Interfaces;

using Application.Features.OAuth.GetAuthUrl.Dtos;

public interface IAuthUrlBuilder
{
    GetAuthUrlResponseDto BuildAuthUrl(string providerName);
}
