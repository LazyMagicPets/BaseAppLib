namespace BaseApp.ViewModels;

using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.

[Factory]
public class PetViewModel : LzItemViewModelAuthNotifications<Pet, PetModel>
{
    public PetViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        [FactoryInject] IHostApi hostApi,
        ILzParentViewModel parentViewModel,
        Pet pet,
        bool? isLoaded = null
        ) : base(loggerFactory, pet, model: null, isLoaded) 
    {
        ParentViewModel = parentViewModel;
        // We use GetMethod to dynamically bind to the methods in the host API to 
        // allow different implementations of the API to be used at runtime for 
        // this viewModel.
        _DTOReadAsync = (Func<string, Task<Pet>>?)hostApi.GetMethod("StoreModuleGetPetByIdAsync", typeof(string));
        _DTOCreateAsync = (Func<Pet, Task<Pet>>?)hostApi.GetMethod("StoreModuleAddPetAsync", typeof(Pet));
        _DTOUpdateAsync = (Func<Pet, Task<Pet>>?)hostApi.GetMethod("StoreModuleUpdatePetAsync", typeof(Pet));  
        _DTODeleteAsync = (Func<string,Task>?)hostApi.GetMethod("StoreModuleDeletePetAsync", typeof(string));
    }
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => Data?.UpdateUtcTick ?? long.MaxValue;

}
