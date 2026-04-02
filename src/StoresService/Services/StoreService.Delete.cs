using Microsoft.EntityFrameworkCore;

namespace StoresService.Services;

public partial class StoreService
{
    public async Task<bool> DeleteAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Stores
            .FirstOrDefaultAsync(store => store.Id == id.Trim(), cancellationToken);

        if (entity is null)
        {
            return false;
        }

        _dbContext.Stores.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
