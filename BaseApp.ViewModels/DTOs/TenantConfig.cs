namespace BaseApp.ViewModels;

public class TenantConfig : ITenantConfig
{
    public string SeeMoreUrl { get; set; } = "https://www.eventtoolkit.app/";
    public string TenantName { get; set; } = "";
}

