namespace BaseApp.ViewModels;

using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.

[Factory]
public class PetViewModel : LzItemViewModelAuthNotifications<Pet, PetModel>
{
    public PetViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        [FactoryInject] ReadPetDelegate? readPetDelegate,
        [FactoryInject] CreatePetDelegate? createPetDelegate,
        [FactoryInject] UpdatePetDelegate? updatePetDelegate,
        [FactoryInject] DeletePetDelegate? deletePetDelegate,
        IBaseAppSessionViewModel sessionViewModel,
        ILzParentViewModel parentViewModel,
        Pet pet,
        bool? isLoaded = null
        ) : base(loggerFactory, sessionViewModel, pet, model: null, isLoaded) 
    {
        _sessionViewModel = sessionViewModel;   
        ParentViewModel = parentViewModel;
        _DTOReadAsync = readPetDelegate?.Value ?? sessionViewModel!.ConsumerApi!.PublicModuleGetPetByIdAsync;
        _DTOCreateAsync = createPetDelegate?.Value; // no default because the API is not available in BaseAppLib
        _DTOUpdateAsync = updatePetDelegate?.Value; // no default because the API is not available in the BaseAppLib
        _DTODeleteAsync = deletePetDelegate?.Value; // no default because the API is not available in the BaseAppLib

    }
    private IBaseAppSessionViewModel _sessionViewModel;
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => Data?.UpdateUtcTick ?? long.MaxValue;

}
