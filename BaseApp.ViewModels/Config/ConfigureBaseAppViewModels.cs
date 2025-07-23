
namespace BaseApp.ViewModels;

public static class ConfigureBaseAppViewModels
{
    public static IServiceCollection AddBaseAppViewModels(this IServiceCollection services)
    {
        //var assembly = typeof(ConfigureBaseAppViewModels).Assembly; // Uncomment if you need to use the assembly for something
        BaseAppViewModelsRegisterFactories.BaseAppViewModelsRegister(services); // Run generated registration code

        services.AddSingleton<ILzClientConfig, LzClientConfig>();
        services.AddSingleton<IConsumerApi, ConsumerApi.ConsumerApi>();

        return services;
    }
}
