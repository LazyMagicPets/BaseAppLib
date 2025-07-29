namespace BlazorUI;
public static partial class ConfigureBlazorUI
{
    public static IServiceCollection AddBlazorUI(this IServiceCollection services)
    {
        //var assembly = typeof(ConfigureBlazorUI).Assembly;

        services.AddBaseAppBlazorUI();
        return services;
    }
}
