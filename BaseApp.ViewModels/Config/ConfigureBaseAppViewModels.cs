
using LazyMagic.Shared;

namespace BaseApp.ViewModels;

public static class ConfigureBaseAppViewModels
{
    public static IServiceCollection AddBaseAppViewModels(this IServiceCollection services)
    {
        //var assembly = typeof(ConfigureBaseAppViewModels).Assembly; // Uncomment if you need to use the assembly for something
        BaseAppViewModelsRegisterFactories.BaseAppViewModelsRegister(services); // Run generated registration code

        services.AddSingleton<ILzMessages, LzMessages>();
        services.AddSingleton<ILzClientConfig, LzClientConfig>();
        services.AddSingleton<ILzHttpClient, LzHttpClientCognito>();
        services.AddLazyMagicAuthCognito();

        return services;
    }
}
