

namespace BlazorUI;


public static class ConfigureBaseAppBlazorUI
{

    public static IServiceCollection AddBaseAppBlazorUI(this IServiceCollection services)
    {
        services.AddLazyMagicBlazor();

        services.AddMudServices();

        return services;
    }
}
