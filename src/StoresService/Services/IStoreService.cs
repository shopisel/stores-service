using StoresService.Contracts;

namespace StoresService.Services;

public interface IStoreService
{
    Task<IEnumerable<StoreResponse>> GetByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);
    Task<IEnumerable<StoreResponse>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<StoreResponse> CreateAsync(CreateStoreRequest request, CancellationToken cancellationToken = default);
    Task<StoreResponse?> UpdateAsync(string id, UpdateStoreRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
}
