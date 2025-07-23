
using Microsoft.Extensions.Logging;

namespace BlazorUI;


public static class ConfigureBaseApp
{

    public static IServiceCollection AddBaseApp(this IServiceCollection services)
    {
        return services
        // Add our components
        .AddSingleton<ILzMessages, LzMessages>()
        .AddSingleton<ILzClientConfig, LzClientConfig>()
        .AddSingleton<IConnectivityService, ConnectivityService>()
        .AddSingleton<IInternetConnectivitySvc>(sp => sp.GetRequiredService<IConnectivityService>())
        .AddSingleton<IOSAccess, BlazorOSAccess>()
        .AddSingleton<ILzJsUtilities, LzJsUtilities>()
        .AddSingleton<BrowserFingerprintService>()

        // Add our viewmodels
        .AddBaseAppViewModels()

        // Add third party components/services

        .AddScoped<ClipboardService>()
        .AddMudServices()
       ;
    }
}
