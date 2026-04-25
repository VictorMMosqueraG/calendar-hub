namespace Application.Interfaces.Services;

using Application.Features.Auth.GetStatus.Dtos;

public interface ITokenStore
{
    void SetToken(string providerName, string accessToken);
    string? GetToken(string providerName);
    void RemoveToken(string providerName);
    GetStatusResponseDto GetConnectedProviders();
}
