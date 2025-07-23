using Newtonsoft.Json.Linq;

namespace BaseApp.ViewModels;

/// <summary>
/// Non-generic base interface that provides access to BaseAppSessionViewModel.
/// This allows BaseApp.BlazorUI components to access the session without knowing the concrete type
/// of the SessionViewModel.
/// </summary>
public interface IBaseAppSessionsViewModelBase
{
    IBaseAppSessionViewModel? BaseAppSessionViewModel { get; }
    ILzJsUtilities? LzJsUtilities { get; set; }
    TenantConfigViewModel? TenantConfigViewModel { get; }
    BrowserFingerprint? BrowserFingerprint { get; set; }
    JObject TenancyConfig { get; set; }
}