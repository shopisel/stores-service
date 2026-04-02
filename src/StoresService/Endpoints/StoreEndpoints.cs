using StoresService.Contracts;
using StoresService.Services;
using Microsoft.AspNetCore.Mvc;

namespace StoresService.Endpoints;

public static class StoreEndpoints
{
    public static void MapStoreEndpoints(this IEndpointRouteBuilder app)
    {
        var stores = app.MapGroup("/stores").WithTags("Store");

        stores.MapGet(string.Empty, async (
            [FromQuery] string? ids,
            [FromQuery] string? name,
            IStoreService storeService,
            CancellationToken ct) =>
        {
            var hasIds = !string.IsNullOrWhiteSpace(ids);
            var hasName = !string.IsNullOrWhiteSpace(name);

            if (!hasIds && !hasName)
            {
                return Results.BadRequest("At least one query filter is required: ids or name.");
            }

            if (hasIds && hasName)
            {
                return Results.BadRequest("When ids is informed it must be the only filter.");
            }

            if (hasIds)
            {
                var idList = ids!
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Distinct()
                    .ToList();

                if (idList.Count == 0)
                {
                    return Results.BadRequest("Invalid ids format.");
                }

                var byIds = await storeService.GetByIdsAsync(idList, ct);
                return Results.Ok(byIds);
            }

            var byName = await storeService.SearchByNameAsync(name!, ct);
            return Results.Ok(byName);
        })
        .WithName("GetStores")
        .WithSummary("Get stores by query filters");

        stores.MapPost(string.Empty, async (
            [FromBody] CreateStoreRequest request,
            IStoreService storeService,
            CancellationToken ct) =>
        {
            try
            {
                var createdStore = await storeService.CreateAsync(request, ct);
                return Results.Created($"/stores/{createdStore.Id}", createdStore);
            }
            catch (ArgumentException ex)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    [ex.ParamName ?? "error"] = [ex.Message]
                });
            }
        })
        .WithName("CreateStore")
        .WithSummary("Create store");

        stores.MapPut("/{storeId}", async (
            string storeId,
            [FromBody] UpdateStoreRequest request,
            IStoreService storeService,
            CancellationToken ct) =>
        {
            try
            {
                var updatedStore = await storeService.UpdateAsync(storeId, request, ct);
                return updatedStore is null ? Results.NotFound() : Results.Ok(updatedStore);
            }
            catch (ArgumentException ex)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    [ex.ParamName ?? "error"] = [ex.Message]
                });
            }
        })
        .WithName("UpdateStore")
        .WithSummary("Update store");

        stores.MapDelete("/{storeId}", async (
            string storeId,
            IStoreService storeService,
            CancellationToken ct) =>
        {
            var success = await storeService.DeleteAsync(storeId, ct);
            return success ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteStore")
        .WithSummary("Delete store");
    }
}
