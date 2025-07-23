namespace ViewModels;

public static class ConfigureViewModels
{
    public static IServiceCollection AddAppViewModels(this IServiceCollection services)
    {

        ViewModelsRegisterFactories.ViewModelsRegister(services); // Register Factory Classes

        services.AddSingleton<SessionsViewModel>();
        services.AddSingleton<ISessionsViewModel>(provider => provider!.GetService<SessionsViewModel>()!);
        services.AddSingleton<IBaseAppSessionsViewModel<ISessionViewModel>>(provider => provider!.GetService<SessionsViewModel>()!);
        services.AddSingleton<IBaseAppSessionsViewModelBase>(provider => provider!.GetService<SessionsViewModel>()!);
        services.AddLazyMagicAuthCognito();
        services.AddBaseAppViewModels();

        // Register wrapper delegates for PetViewModel to disambiguate operations
        
        // ReadPetDelegate - delegates to ConsumerApi
        services.AddSingleton<ReadPetDelegate>(provider => 
        {
            var sessionsViewModel = provider.GetRequiredService<ISessionsViewModel>();
            Func<string, Task<Pet>> func = async (id) => 
            {
                var session = sessionsViewModel.BaseAppSessionViewModel as ISessionViewModel;
                if (session?.ConsumerApi == null) 
                    throw new InvalidOperationException("ConsumerApi not available");
                return await session.ConsumerApi.PublicModuleGetPetByIdAsync(id);
            };
            return new ReadPetDelegate(func);
        });
        
        // CreatePetDelegate - not available in BaseAppLib, so provide exception-throwing implementation
        services.AddSingleton<CreatePetDelegate>(provider => 
            new CreatePetDelegate((pet) => Task.FromException<Pet>(new NotImplementedException("Pet create not implemented"))));
        
        // UpdatePetDelegate - not available in BaseAppLib, so provide exception-throwing implementation  
        services.AddSingleton<UpdatePetDelegate>(provider => 
            new UpdatePetDelegate((pet) => Task.FromException<Pet>(new NotImplementedException("Pet update not implemented"))));
        
        // DeletePetDelegate - not available in BaseAppLib, so provide exception-throwing implementation
        services.AddSingleton<DeletePetDelegate>(provider => 
            new DeletePetDelegate((id) => Task.FromException(new NotImplementedException("Pet delete not implemented"))));

        return services;
    }
}

