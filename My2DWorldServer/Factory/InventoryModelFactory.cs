using Microsoft.EntityFrameworkCore;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
using My2DWorldShared.Enums;
using My2DWorldShared.Extensions;
using My2DWorldShared.Models;

namespace My2DWorldServer.Factory
{
    public class InventoryModelFactory
    {
        private IDbContextFactory<SqlDbContext> _dbContextFactory;
        private UserSession _session;
        public InventoryModelFactory(IDbContextFactory<SqlDbContext> dbContextFactory, UserSession session)
        {
            _dbContextFactory = dbContextFactory;
            _session = session;
        }

        public async Task<InventoryModel> Create(int offset, int fetchCount = 50)
        {
            if (_session.Logged)
            {
                using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    UserEntity? user = await dbContext.Users.FindAsync(_session.UserId);
                    if(user != null)
                    {
                        var inventoryItems = user.Inventory?.Reverse().Skip(offset).Take(fetchCount).Select(x => new ItemInfoModel
                        {
                            ItemId = x.ItemId,
                            Name = x.Item?.Name,
                            Type = x.Item?.Type,
                            PriceType = x.Item?.PriceType,
                            Price = x.Item?.Price,
                            FilePath = x.Item?.FilePath,
                            ItemDesc = x.Item?.ItemDesc,
                            ItemQuantity = x.ItemQuantity
                        }).ToArray() ?? Array.Empty<ItemInfoModel>();

                        var inventoryStats = new InventoryBatchInfoModel
                        {
                            ItemsCount = user.Inventory?.Count,
                            MaxItemsPerBatch = fetchCount,
                            ItemsBatchCount = inventoryItems.Length
                        };

                        return new InventoryModel
                        {
                            Batch = inventoryItems,
                            Info = inventoryStats
                        };
                    }
                }
            }
            return new InventoryModel();
        }
    }
}
