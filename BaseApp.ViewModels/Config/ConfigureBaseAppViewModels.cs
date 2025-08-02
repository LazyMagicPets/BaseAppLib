
using LazyMagic.Shared;

namespace BaseApp.ViewModels;

public static class ConfigureBaseAppViewModels
{
    public static IServiceCollection AddBaseAppViewModels(this IServiceCollection services)
    {
        //var assembly = typeof(ConfigureBaseAppViewModels).Assembly; // Uncomment if you need to use the assembly for something
        BaseAppViewModelsRegisterFactories.BaseAppViewModelsRegister(services); // Run generated registration code

        services.TryAddSingleton<ILzMessages, LzMessages>();
        services.TryAddSingleton<ILzClientConfig, LzClientConfig>();
        services.TryAddSingleton<ILzHttpClient, LzHttpClientCognito>();
        services.AddLazyMagicAuthCognito();

        // Use of the various module clients is optional so we register them as singletons with null values.
        // Note we are using TryAdd so any existing registrations will not be overridden. It is expected that 
        // the application will register its own implementations of these interfaces if needed.
        services.TryAddSingleton<IPublicModuleClient>(provider => null!);
        services.TryAddSingleton<IConsumerModuleClient>(provider => null!);
        services.TryAddSingleton<IStoreModuleClient>(provider => null!);
        services.TryAddSingleton<IAdminModuleClient>(provider => null!);

        return services;
    }
}
 