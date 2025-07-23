using LazyMagic.Client.Auth;
using Newtonsoft.Json;

namespace ViewModels;
/// <summary>
/// The SessionViewModel is the root viewModel for a user session.
/// This class maintains the "state" of the use session, which includes 
/// the data (in this case the sets).
/// </summary>
[Factory]

public class SessionViewModel : BaseAppSessionViewModel, ISessionViewModel
{
    public SessionViewModel(
        [FactoryInject] ILoggerFactory loggerFactory, // singleton
        [FactoryInject] ILzClientConfig clientConfig, // singleton
        [FactoryInject] IInternetConnectivitySvc internetConnectivity, // singleton
        [FactoryInject] ILzHost lzHost, // singleton
        [FactoryInject] ILzMessages messages, // singleton
        [FactoryInject] IAuthProcess authProcess, // transient
        [FactoryInject] IPetsViewModelFactory petsViewModelFactory, // transient
        [FactoryInject] ICategoriesViewModelFactory categoriesViewModelFactory, // transient
        [FactoryInject] ITagsViewModelFactory tagsViewModelFactory, // transient
        ISessionsViewModel sessionsViewModel
        ) : base(loggerFactory, authProcess, clientConfig, internetConnectivity, messages, 
                petsViewModelFactory, categoriesViewModelFactory, tagsViewModelFactory )
    {
        this.sessionsViewModel = sessionsViewModel;

        try
        {

            var sessionId = Guid.NewGuid().ToString();
            ConsumerApi = new ConsumerApi.ConsumerApi(new LzHttpClient(loggerFactory, authProcess.AuthProvider, lzHost, sessionId));

            var tenantKey = (string?)clientConfig.TenancyConfig["tenantKey"] ?? throw new Exception("Cognito TenancyConfig.tenantKey is null");
            authProcess.SetAuthenticator(clientConfig.AuthConfigs?["TenantAuth"]!);
            authProcess.SetSignUpAllowed(false);


            // The PublicApi is used to send fingerPrint data to the server. It is an unauthenticated API client.
            PublicApi = new PublicApi.PublicApi(new LzHttpClient(loggerFactory, null, lzHost, sessionId));

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating SetCalculatorsViewModel: {ex.Message}");
            throw;
        }
    }

    private ISessionsViewModel? sessionsViewModel;

    public IPublicApi PublicApi { get; set; } = null!;  
    public override async Task InitAsync()
    {
        try
        {
            var fingerPrint = JsonConvert.SerializeObject(sessionsViewModel!.BrowserFingerprint);

            PublicSchema.Fingerprint newFingerprint = JsonConvert.DeserializeObject<PublicSchema.Fingerprint>(fingerPrint) ?? new PublicSchema.Fingerprint();

            newFingerprint.Id = Guid.NewGuid().ToString();
            await PublicApi.PublicModuleFingerprintCreateAsync(newFingerprint);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Calling PublicApi: {ex.Message}");
        }
    }
}
