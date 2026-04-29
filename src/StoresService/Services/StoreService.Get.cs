using StoresService.Contracts;
using Microsoft.EntityFrameworkCore;

namespace StoresService.Services;

public partial class StoreService
{
    public async Task<IEnumerable<StoreResponse>> GetByBrandsAsync(
        IEnumerable<string> brands,
        CancellationToken cancellationToken = default)
    {
        var brandList = brands
            .Where(brand => !string.IsNullOrWhiteSpace(brand))
            .Select(brand => brand.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (brandList.Count == 0)
        {
            return [];
        }

        var stores = await _dbContext.Stores
            .AsNoTracking()
            .Where(store => store.Brand != null && brandList.Contains(store.Brand))
            .OrderBy(store => store.Brand)
            .ThenBy(store => store.City)
            .ThenBy(store => store.Name)
            .ToListAsync(cancellationToken);

        return stores.Select(MapToResponse);
    }

    public async Task<IEnumerable<StoreResponse>> GetByIdsAsync(
        IEnumerable<string> ids,
        CancellationToken cancellationToken = default)
    {
        var idList = ids
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id.Trim())
            .Distinct(StringComparer.Ordinal)
            .ToList();

        if (idList.Count == 0)
        {
            return [];
        }

        var stores = await _dbContext.Stores
            .AsNoTracking()
            .Where(store => idList.Contains(store.Id))
            .OrderBy(store => store.Name)
            .ToListAsync(cancellationToken);

        return stores.Select(MapToResponse);
    }

    public async Task<IEnumerable<StoreResponse>> SearchByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        var normalizedName = name.Trim().ToLower();

        var stores = await _dbContext.Stores
            .AsNoTracking()
            .Where(store =>
                store.Name.ToLower().Contains(normalizedName) ||
                (store.Brand != null && store.Brand.ToLower().Contains(normalizedName)) ||
                (store.City != null && store.City.ToLower().Contains(normalizedName)) ||
                (store.Address != null && store.Address.ToLower().Contains(normalizedName)))
            .OrderBy(store => store.Name)
            .ToListAsync(cancellationToken);

        return stores.Select(MapToResponse);
    }
}
