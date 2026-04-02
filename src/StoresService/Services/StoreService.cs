using StoresService.Contracts;
using StoresService.Data;

namespace StoresService.Services;

public partial class StoreService(StoresServiceDbContext dbContext) : IStoreService
{
    private readonly StoresServiceDbContext _dbContext = dbContext;

    private static string GenerateStoreId() => $"store_{Guid.NewGuid():N}";

    private static StoreResponse MapToResponse(Data.Entities.StoreEntity entity) =>
        new(entity.Id, entity.Name);
}
