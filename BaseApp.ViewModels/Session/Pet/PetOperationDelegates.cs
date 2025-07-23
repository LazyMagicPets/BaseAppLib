namespace BaseApp.ViewModels;

/// <summary>
/// Wrapper types to disambiguate Pet CRUD operations with identical signatures
/// </summary>

/// <summary>
/// Wraps the Pet read operation delegate
/// </summary>
public record ReadPetDelegate(Func<string, Task<Pet>>? Value);

/// <summary>
/// Wraps the Pet create operation delegate
/// </summary>
public record CreatePetDelegate(Func<Pet, Task<Pet>>? Value);

/// <summary>
/// Wraps the Pet update operation delegate
/// </summary>
public record UpdatePetDelegate(Func<Pet, Task<Pet>>? Value);

/// <summary>
/// Wraps the Pet delete operation delegate
/// </summary>
public record DeletePetDelegate(Func<string, Task>? Value);