using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
using System.Text.Json;

namespace My2DWorldServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private IDbContextFactory<SqlDbContext> _sqlContextFactory;
        public InventoryController(IDbContextFactory<SqlDbContext> sqlContextFactory)
        {
            _sqlContextFactory = sqlContextFactory;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            using (var dbContext = await _sqlContextFactory.CreateDbContextAsync())
            {
                UserEntity? user = await dbContext.Users.FindAsync(1);
                if (user != null && user.Inventory != null)
                {
                    return JsonSerializer.Serialize(user.Inventory.Count);
                }
            }
            return "err";
        }
    }
}
