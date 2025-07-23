namespace BaseApp.ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.   
[Factory]
public class CategoryViewModel : LzItemViewModelAuthNotifications<Category, CategoryModel>
{
    public CategoryViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        IBaseAppSessionViewModel sessionViewModel,
        ILzParentViewModel parentViewModel,
        Category category,
        bool? isLoaded = null
        ) : base(loggerFactory, sessionViewModel, category, model: null, isLoaded)
    {
        _sessionViewModel = sessionViewModel;
        ParentViewModel = parentViewModel;
        //_DTOCreateAsync = sessionViewModel.Store.AddCategoryAsync;
        //_DTOReadAsync = sessionViewModel.ConsumerApi.GetCategoryByIdAsync;
        //_DTOUpdateAsync = sessionViewModel.Store.UpdateCategoryAsync;
        //_DTODeleteAsync = sessionViewModel.Store.DeleteCategoryAsync;
    }
    private IBaseAppSessionViewModel _sessionViewModel;
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => long.MaxValue;
}
