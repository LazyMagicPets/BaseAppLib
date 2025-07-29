using LazyMagic.Client.Auth;
using Newtonsoft.Json;

namespace ViewModels;
/// <summary>
/// The SessionViewModel is the root viewModel for a user session.
/// This class maintains the "state" of the use session, which includes 
/// the data (in this case the sets).
/// </summary>
[Factory]

public class SessionViewModel : BaseAppSessionViewModel, ISessionViewModel, ICurrentSessionViewModel
{
    public SessionViewModel(
        [FactoryInject] ILoggerFactory loggerFactory, // singleton
        [FactoryInject] ILzClientConfig clientConfig, // singleton
        [FactoryInject] IConnectivityService connectivityService, // singleton
        [FactoryInject] ILzHost lzHost, // singleton
        [FactoryInject] ILzMessages messages, // singleton
        [FactoryInject] IAuthProcess authProcess, // transient
        [FactoryInject] IPetsViewModelFactory petsViewModelFactory, // transient
        [FactoryInject] ICategoriesViewModelFactory categoriesViewModelFactory, // transient
        [FactoryInject] ITagsViewModelFactory tagsViewModelFactory, // transient
        [FactoryInject] Lazy<IStoreApi> storeApi, // singleton
        ISessionsViewModel sessionsViewModel
        ) : base(loggerFactory, authProcess, clientConfig, connectivityService, messages, 
                petsViewModelFactory, categoriesViewModelFactory, tagsViewModelFactory )
    {
        this.sessionsViewModel = sessionsViewModel;

        try
        {
            // Note that StoreApi has a dependency on LzHttpClient, which has a dependency on ISessionsViewModel.
            // The LzHttpClient uses Lazy<ISessionsViewModel> to avoid a circular dependency. This works just fine 
            // as we make no calls to the StoreApi until after the SessionViewModel is fully initialized. 
            
            var tenantKey = (string?)clientConfig.TenancyConfig["tenantKey"] ?? throw new Exception("Cognito TenancyConfig.tenantKey is null");
            authProcess.SetAuthenticator(clientConfig.AuthConfigs?["TenantAuth"]!);
            authProcess.SetSignUpAllowed(false);

            // Note: IStoreApi is resolved when IHostApi is resolved. This makes dynamice binding of the API methods possible
            // in libraries like the BaseApp.ViewModels. In the app, use the IStoreApi interface to access the StoreApi methods 
            // to avoid the overhead of the dynamic binding.
            _storeApi = storeApi?.Value ?? throw new ArgumentNullException(nameof(storeApi), "StoreApi cannot be null");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating SetCalculatorsViewModel: {ex.Message}");
            throw;
        }
    }

    private ISessionsViewModel? sessionsViewModel;
    protected IStoreApi _storeApi;
    public override async Task InitAsync()
    {
        try
        {
            var fingerPrint = JsonConvert.SerializeObject(sessionsViewModel!.BrowserFingerprint);

            PublicSchema.Fingerprint newFingerprint = JsonConvert.DeserializeObject<PublicSchema.Fingerprint>(fingerPrint) ?? new PublicSchema.Fingerprint();

            newFingerprint.Id = Guid.NewGuid().ToString();
            await _storeApi.PublicModuleFingerprintCreateAsync(newFingerprint);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Calling PublicApi: {ex.Message}");
        }
    }
}
