namespace BaseApp.ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
public abstract class BaseAppSessionViewModel : 
    LzSessionViewModelAuthNotifications, 
    IBaseAppSessionViewModel
{
    public BaseAppSessionViewModel(
        ILoggerFactory loggerFactory,
        IAuthProcess authProcess,
        ILzClientConfig clientConfig,
        IConnectivityService connectivityService,
        ILzMessages messages,
        IPetsViewModelFactory petsViewModelFactory, // transient
        ICategoriesViewModelFactory categoriesViewModelFactory, // transient
        ITagsViewModelFactory tagsViewModelFactory // transient
        ) : base(loggerFactory, authProcess, clientConfig, connectivityService, messages)
    {

        PetsViewModel = petsViewModelFactory?.Create() ?? throw new ArgumentNullException(nameof(petsViewModelFactory));

        CategoriesViewModel = categoriesViewModelFactory?.Create() ?? throw new ArgumentNullException(nameof(categoriesViewModelFactory));

        TagsViewModel = tagsViewModelFactory?.Create() ?? throw new ArgumentNullException(nameof(tagsViewModelFactory));
    }

    public PetsViewModel PetsViewModel { get; set; }
    public CategoriesViewModel CategoriesViewModel { get; set; }
    public TagsViewModel TagsViewModel { get; set; }

}
