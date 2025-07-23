namespace ViewModels;

/// <summary>
/// This looks redundant but its not. Each app may have multiple libraries that 
/// provide SessionViewModels. We reference each of those here, in this case 
/// we only have one because the SnapsApp only references the SetsCmp.ViewModels library.
/// </summary>
public interface ISessionViewModel :
    IBaseAppSessionViewModel
{



}
