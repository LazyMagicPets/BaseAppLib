using LazyMagic.Shared;

namespace ViewModels;

public static class ConfigureViewModels
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {

        ViewModelsRegisterFactories.ViewModelsRegister(services); // Register Factory Classes


        // Register the ClientSDK 
        services.AddSingleton<IAdminApi> (serviceProvider =>
        {
            var lzHost = serviceProvider.GetRequiredService<ILzHost>();
            var authenticationHandler = serviceProvider.GetRequiredService<IAuthenticationHandler>();
            var handler = authenticationHandler.CreateHandler();
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(lzHost.GetApiUrl("")) // LocalApiUrl or RemoteApiUrl depending on UseLocalhostApi property
            };
            var api = new AdminApi.AdminApi(httpClient);
            return api;
        });

        // Register the modules used from the Client SDK.
        services.AddSingleton<IPublicModuleClient>(provider => provider.GetRequiredService<IAdminApi>());
        services.AddSingleton<IConsumerModuleClient>(provider => provider.GetRequiredService<IAdminApi>());
        services.AddSingleton<IStoreModuleClient>(provider => provider.GetRequiredService<IAdminApi>());

        services.AddSingleton<ISessionViewModel, SessionViewModel>();

        services.AddBaseAppViewModels();
           
        return services;
    }
}

