namespace BaseApp.ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
using PublicModule;

[Factory]
public class TagsViewModel : LzItemsViewModel<TagViewModel, Tag, TagModel>
{
    public TagsViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        [FactoryInject] IPublicModuleClient publicApi,   
        [FactoryInject] ITagViewModelFactory tagViewModelFactory) : base(loggerFactory)
    {
        TagViewModelFactory = tagViewModelFactory;
        _DTOReadListAsync = publicApi.PublicModuleGetPetTagsAsync;
    }
    public ITagViewModelFactory? TagViewModelFactory { get; init; }
    /// <inheritdoc/>
    public override (TagViewModel, string) NewViewModel(Tag dto)
        => (TagViewModelFactory!.Create(this, dto), string.Empty);
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
