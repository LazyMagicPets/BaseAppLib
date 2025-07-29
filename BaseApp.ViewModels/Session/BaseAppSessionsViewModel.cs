using LazyMagic.Client.Base;
using Newtonsoft.Json.Linq;

namespace BaseApp.ViewModels;

public abstract class BaseAppSessionsViewModel<T> : 
    LzSessionsViewModelAuthNotifications<T>, 
    IBaseAppSessionsViewModel<T>
    where T : IBaseAppSessionViewModel

{
    public BaseAppSessionsViewModel(
        ILoggerFactory loggerFactory,
        ITenantConfigViewModelFactory tenantConfigViewModelFactory,
        IStaticAssets staticAssets,
        ILzJsUtilities lzJsUtilities

        ) : base(loggerFactory)
    {
        TenantConfigViewModel = tenantConfigViewModelFactory.Create(staticAssets);
        LzJsUtilities = lzJsUtilities;
    }
    public ILzJsUtilities? LzJsUtilities { get; set; }
    public TenantConfigViewModel TenantConfigViewModel { get; init; }
    public BrowserFingerprint? BrowserFingerprint { get; set; } // This is used to send the browser fingerprint to the server.

    public JObject TenancyConfig { get; set; } = new JObject();

    // Implement IBaseAppSessionsViewModelBase.BaseAppSessionViewModel
    // Cast the generic SessionViewModel to IBaseAppSessionViewModel
    public IBaseAppSessionViewModel? BaseAppSessionViewModel => SessionViewModel as IBaseAppSessionViewModel;

}
