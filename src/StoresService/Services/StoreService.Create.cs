using StoresService.Contracts;
using StoresService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace StoresService.Services;

public partial class StoreService
{
    public async Task<StoreResponse> CreateAsync(
        CreateStoreRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("The store name is required.", nameof(request));
        }

        var storeName = request.Name.Trim();

        var entity = new StoreEntity
        {
            Id = GenerateStoreId(),
            Name = storeName
        };

        _dbContext.Stores.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToResponse(entity);
    }
}
