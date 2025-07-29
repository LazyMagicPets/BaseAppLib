namespace BaseApp.ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
[Factory]
public class TagViewModel : LzItemViewModelAuthNotifications<Tag, TagModel>
{
    public TagViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        ILzParentViewModel parentViewModel,
        Tag tag,
        bool? isLoaded = null
        ) : base(loggerFactory, tag, model: null, isLoaded)
    {
        ParentViewModel = parentViewModel;
    }
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => long.MaxValue;
}
