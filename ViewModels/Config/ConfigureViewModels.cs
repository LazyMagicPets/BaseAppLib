using LazyMagic.Shared;

namespace ViewModels;

public static class ConfigureViewModels
{
    public static IServiceCollection AddAppViewModels(this IServiceCollection services)
    {

        ViewModelsRegisterFactories.ViewModelsRegister(services); // Register Factory Classes

        services.AddBaseAppViewModels();

        // ISessionsViewModel serves as the global app state. It manages multiple 
        // sessions, each represented by a SessionViewModel. This is useful for 
        // POS terminal apps where multiple sessions can be active at once. PWAa 
        // and mobile apps are generally single session apps so there will be only
        // a single SessionViewModel for those app types.
        services.AddSingleton<ISessionsViewModel, SessionsViewModel>();

        // Register the SessionsViewModel as the base app sessions view model for BaseApp.ViewModel library use.
        services.AddSingleton<IBaseAppSessionsViewModel<ISessionViewModel>>(provider => provider!.GetService<SessionsViewModel>()!);
        services.AddSingleton<IBaseAppSessionsViewModelBase>(provider => provider!.GetService<SessionsViewModel>()!);

        services.AddSingleton<IStoreApi> (serviceProvider =>
        {
            // Get the SessionsViewModel from the service provider and then 
            // return the HostApi from it.
            var lzHttpClient = serviceProvider.GetRequiredService<ILzHttpClient>();    
            var storeApi = new StoreApi.StoreApi(lzHttpClient);
            return storeApi;
        });

        // The IHostApi allows the BaseApp.ViewModels library to access the StoreApi methods
        // without needing to know about the StoreApi interface. This is useful for 
        // dynamic binding of the API methods in libraries like BaseApp.ViewModels.
        services.AddTransient<IHostApi>(serviceProvider =>
        {
            var hostApi = serviceProvider.GetRequiredService<IStoreApi>() as IHostApi;
            return hostApi!;
        });

        return services;
    }
}

