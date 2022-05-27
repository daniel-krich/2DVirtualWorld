using Microsoft.EntityFrameworkCore;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
using My2DWorldShared.Enums;
using My2DWorldShared.Extensions;
using My2DWorldShared.Models;

namespace My2DWorldServer.Factory
{
    public class InventoryBatchFactory
    {
        private IDbContextFactory<SqlDbContext> _dbContextFactory;
        private UserSession _session;
        public InventoryBatchFactory(IDbContextFactory<SqlDbContext> dbContextFactory, UserSession session)
        {
            _dbContextFactory = dbContextFactory;
            _session = session;
        }

        public async Task<InventoryItemModel[]> Create(int offset, int fetch = 50)
        {
            if (_session.Logged)
            {
                using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    UserEntity? user = await dbContext.Users.FindAsync(_session.UserId);
                    if(user != null)
                    {
                        return user.Inventory?.Skip(offset).Take(fetch).Select(x => new InventoryItemModel
                        {
                            ItemId = x.ItemId,
                            Name = x.Item?.Name,
                            Type = x.Item?.Type,
                            PriceType = x.Item?.PriceType,
                            Price = x.Item?.Price,
                            FilePath = x.Item?.FilePath,
                            ItemDesc = x.Item?.ItemDesc,
                            ItemQuantity = x.ItemQuantity
                        }).ToArray() ?? Array.Empty<InventoryItemModel>();
                    }
                }
            }
            return Array.Empty<InventoryItemModel>();
        }
    }
}
