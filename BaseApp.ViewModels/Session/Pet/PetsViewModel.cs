namespace BaseApp.ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
[Factory]
/// <inheritdoc/>
public class PetsViewModel : LzItemsViewModelAuthNotifications<PetViewModel, Pet, PetModel>
{
    public PetsViewModel(
        [FactoryInject]ILoggerFactory loggerFactory,
        [FactoryInject] IPublicModuleClient publicApi,
        [FactoryInject] IPetViewModelFactory petViewModelFactory) : base(loggerFactory)  
    { 
        PetViewModelFactory = petViewModelFactory;
        _DTOReadListAsync = publicApi.PublicModuleListPetsAsync;
    }
    public IPetViewModelFactory? PetViewModelFactory { get; init; }
    /// <inheritdoc/>
    public override (PetViewModel, string) NewViewModel(Pet dto)
        => (PetViewModelFactory!.Create( this, dto), string.Empty);
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


