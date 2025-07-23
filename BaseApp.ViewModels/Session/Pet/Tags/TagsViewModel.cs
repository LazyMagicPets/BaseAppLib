namespace BaseApp.ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
[Factory]
public class TagsViewModel : LzItemsViewModelAuthNotifications<TagViewModel, Tag, TagModel>
{
    public TagsViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        IBaseAppSessionViewModel sessionViewModel,
        [FactoryInject] ITagViewModelFactory tagViewModelFactory) : base(loggerFactory, sessionViewModel)
    {
        _sessionViewModel = sessionViewModel;
        TagViewModelFactory = tagViewModelFactory;
        _DTOReadListAsync = sessionViewModel.ConsumerApi.PublicModuleGetPetTagsAsync;
    }
    private IBaseAppSessionViewModel _sessionViewModel;
    public ITagViewModelFactory? TagViewModelFactory { get; init; }
    /// <inheritdoc/>
    public override (TagViewModel, string) NewViewModel(Tag dto)
        => (TagViewModelFactory!.Create(_sessionViewModel, this, dto), string.Empty);
    public Func<Task<string>>? SvcTestAsync { get; init; }
    public async Task<string> TestAsync()
    {
        if (SvcTestAsync is null)
            return string.Empty;
        return await SvcTestAsync();
    }
    /// <inheritdoc/>
    public override async Task<(bool, string)> ReadAsync(bool forceload = false)
    => await base.ReadAsync(forceload);

}
