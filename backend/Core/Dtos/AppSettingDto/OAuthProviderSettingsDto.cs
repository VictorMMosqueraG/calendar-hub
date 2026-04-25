namespace Core.Dtos.AppSettingDto;

public class OAuthSettingsDto
{
    public OAuthProviderSettingsDto? Google { get; set; }
}

public class OAuthProviderSettingsDto
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? RedirectUri { get; set; }
    public string? AuthUrl { get; set; }
    public string? TokenUrl { get; set; }
    public string? Scope { get; set; }
}
