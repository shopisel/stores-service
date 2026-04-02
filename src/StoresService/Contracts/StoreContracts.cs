namespace StoresService.Contracts;

public sealed record StoreResponse(
    string Id,
    string Name);

public sealed record CreateStoreRequest(
    string Name);

public sealed record UpdateStoreRequest(
    string Name);
