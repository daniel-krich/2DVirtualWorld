using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using My2DWorldServer.Extensions;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
using My2DWorldShared.DTOs;
using My2DWorldShared.Enums;
using System.Text.Json;

namespace My2DWorldServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private IDbContextFactory<SqlDbContext> _sqlContextFactory;
        public RegisterController(IDbContextFactory<SqlDbContext> sqlContextFactory)
        {
            _sqlContextFactory = sqlContextFactory;
        }

        [HttpPost]
        public async Task<string> Register([FromBody] RegisterRequestDTO request)
        {
            using (var dbContext = await _sqlContextFactory.CreateDbContextAsync())
            {
                try
                {
                    EntityEntry<UserEntity> user = await dbContext.Users.AddAsync(new UserEntity
                    {
                        Username = request.Username,
                        Password = request.Password,
                        Email = request.Email,
                        Gender = request.Gender,
                        EyeColor = request.EyeColor,
                        SkinTone = request.SkinColor,
                        Birthday = request.Birthday,
                        Hair = request.Gender == GenderType.Male ? 4 : 2
                    });
                    await dbContext.SaveChangesAsync();

                    await dbContext.UsersInventory.AddAsync(new UserInventoryEntity
                    {
                        ItemId = request.Gender == GenderType.Male ? 4 : 2,
                        UserId = user.Entity.Id,
                        ItemQuantity = 1
                    });
                    await dbContext.SaveChangesAsync();
                    return JsonSerializerExtensions.SerializeUnicode(new RegisterResponseDTO
                    {
                        Success = true
                    });
                }
                catch
                {
                    return JsonSerializerExtensions.SerializeUnicode(new RegisterResponseDTO
                    {
                        ErrorString = "שם המשתמש או הכתובת מייל קיימים כבר במערכת"
                    });
                }
            }
        }
    }
}
