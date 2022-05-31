using Microsoft.EntityFrameworkCore;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
using My2DWorldShared.Enums;
using My2DWorldShared.Extensions;
using My2DWorldShared.Models;

namespace My2DWorldServer.Factory
{
    public class ShopModelFactory
    {
        private IDbContextFactory<SqlDbContext> _dbContextFactory;
        public ShopModelFactory(IDbContextFactory<SqlDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ShopModel> Create(int shopId, int shopPage, int fetchCount)
        {
            using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
            {
                ShopEntity? shop = await dbContext.Shops.FindAsync(shopId);
                ItemInfoModel[]? shopItems = shop?.ShopItems?.Skip((shopPage-1) * fetchCount).Take(fetchCount).Select(x => new ItemInfoModel
                {
                    ItemId = x.ItemId,
                    Name = x.Item?.Name,
                    Type = x.Item?.Type,
                    PriceType = x.Item?.PriceType,
                    Price = x.Item?.Price,
                    FilePath = x.Item?.FilePath,
                    ItemDesc = x.Item?.ItemDesc
                }).ToArray();
                ShopModel shopModel = new ShopModel
                {
                    ShopId = shop?.Id,
                    ShopName = shop?.ShopName,
                    Batch = shopItems,
                    Info = new ShopBatchInfoModel
                    {
                        ItemsCount = shop?.ShopItems?.Count,
                        MaxItemsPerBatch = fetchCount,
                        ItemsBatchCount = shopItems?.Length
                    }
                };
                return shopModel;
            }
        }
    }
}
