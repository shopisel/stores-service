namespace StoresService.Contracts;

public sealed record StoreResponse(
    string Id,
    string Name,
    string? Brand,
    string? Address,
    string? City,
    double? Latitude,
    double? Longitude);

public sealed record CreateStoreRequest(
    string Name,
    string? Brand,
    string? Address,
    string? City,
    double? Latitude,
    double? Longitude);

public sealed record UpdateStoreRequest(
    string Name,
    string? Brand,
    string? Address,
    string? City,
    double? Latitude,
    double? Longitude);
