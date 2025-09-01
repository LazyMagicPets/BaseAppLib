namespace BaseApp.ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.   
[Factory]
public class CategoryViewModel : LzItemViewModel<Category, CategoryModel>
{
    public CategoryViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        ILzParentViewModel parentViewModel,
        Category category,
        bool? isLoaded = null
        ) : base(loggerFactory, category, model: null, isLoaded)
    {
        ParentViewModel = parentViewModel;
    }
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => long.MaxValue;
}
