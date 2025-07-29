namespace BaseApp.ViewModels;

/// <summary>
/// IHostApi is an interface that allows DI to inject an implementation of the host API into view models.
/// It can be any of the host APIs that are registered in the DI container. The host API is configured 
/// in the parent applications ViewModel ConfigureViewModels method. We use dynamic API method binding 
/// to allow different implementations of the API to be used at runtime. See MethodExtensions.cs.
/// </summary>
public interface IHostApi
{
}
