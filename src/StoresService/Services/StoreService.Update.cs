using StoresService.Contracts;
using Microsoft.EntityFrameworkCore;

namespace StoresService.Services;

public partial class StoreService
{
    public async Task<StoreResponse?> UpdateAsync(
        string id,
        UpdateStoreRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("The store name is required.", nameof(request));
        }

        var entity = await _dbContext.Stores
            .FirstOrDefaultAsync(store => store.Id == id.Trim(), cancellationToken);

        if (entity is null)
        {
            return null;
        }

        entity.Name = request.Name.Trim();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToResponse(entity);
    }
}
