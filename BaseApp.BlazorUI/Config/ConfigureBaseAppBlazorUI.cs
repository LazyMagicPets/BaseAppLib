
using Microsoft.Extensions.Logging;

namespace BlazorUI;


public static class ConfigureBaseAppBlazorUI
{

    public static IServiceCollection AddBaseAppBlazorUI(this IServiceCollection services)
    {
        return services
        // Add our components
        .AddSingleton<IConnectivityService, ConnectivityService>()
        .AddSingleton<IInternetConnectivitySvc>(sp => sp.GetRequiredService<IConnectivityService>())
        .AddSingleton<IOSAccess, BlazorOSAccess>()
        .AddSingleton<ILzJsUtilities, LzJsUtilities>()
        .AddSingleton<BrowserFingerprintService>()

        // Add third party components/services
        .AddScoped<ClipboardService>()
        .AddMudServices()
       ;
    }
}
