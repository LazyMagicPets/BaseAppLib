using Newtonsoft.Json.Linq;

namespace BaseApp.ViewModels;

public interface IBaseAppSessionsViewModel<T> : 
    ILzSessionsViewModelAuthNotifications<T>,
    IBaseAppSessionsViewModelBase
    where T : IBaseAppSessionViewModel
{

}
