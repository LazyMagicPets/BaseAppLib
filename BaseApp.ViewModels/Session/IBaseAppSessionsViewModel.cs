using Newtonsoft.Json.Linq;

namespace BaseApp.ViewModels;

public interface IBaseAppSessionsViewModel<T> : 
    ILzSessionsViewModelAuthNotifications<T>,
    IBaseAppSessionsViewModelBase
    where T : IBaseAppSessionViewModel
{
    new ILzJsUtilities? LzJsUtilities { get; set; }
    new TenantConfigViewModel? TenantConfigViewModel { get; }
    new BrowserFingerprint? BrowserFingerprint { get; set; }
    new JObject TenancyConfig { get; set; }
}
