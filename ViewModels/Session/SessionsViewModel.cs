
namespace ViewModels;
/// <summary>
/// This class is injected in most pages and is used to manage the current session.
/// SessionViewModel is the current session. 
/// SnapsApp does not support logins so there is only ever one session.
/// </summary>
public class SessionsViewModel : BaseAppSessionsViewModel<ISessionViewModel>, ISessionsViewModel
{
    public SessionsViewModel(
        ILoggerFactory loggerFactory,
        ITenantConfigViewModelFactory tenantConfigViewModelFactory,
        IStaticAssets staticAssets,
        ILzJsUtilities lzJsUtilities,
        ISessionViewModelFactory sessionViewModelFactory
        ) : base(loggerFactory, tenantConfigViewModelFactory, staticAssets, lzJsUtilities) 
    {
        _sessionViewModelFactory = sessionViewModelFactory;
        
        IsInitialized = true;

    }
    private ISessionViewModelFactory _sessionViewModelFactory;
    
    public override ISessionViewModel CreateSessionViewModel()
    {
        var sessionViewModel = _sessionViewModelFactory.Create(this);
        return sessionViewModel;
    }

    // ReadConfigAsync is called from InitAsync() just prior to the IsInitialized being set to true.

}
