using Microsoft.EntityFrameworkCore;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
using My2DWorldShared.Enums;
using My2DWorldShared.Extensions;
using My2DWorldShared.Models;

namespace My2DWorldServer.Factory
{
    public class UserInformationFactory
    {
        private IDbContextFactory<SqlDbContext> _dbContextFactory;
        private UsersSessionCollection _users;
        public UserInformationFactory(IDbContextFactory<SqlDbContext> dbContextFactory, UsersSessionCollection users)
        {
            _dbContextFactory = dbContextFactory;
            _users = users;
        }

        public async Task<UserInformationModel?> Create(UserEntity? user)
        {
            if (user != null)
            {
                using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    PlayerPosition? pos = _users.Sessions.FirstOrDefault(x => x.UserId == user.Id)?.Position;
                    var userInfo = new UserInformationModel
                    {
                        PlayerName = user.Username,
                        PositionX = pos?.X ?? 0,
                        PositionY = pos?.Y ?? 0,
                        PhysicalInfo = new PhysicalInfoModel
                        {
                            Gender = (GenderType)user.Gender,
                            EyeColor = user.EyeColor,
                            SkinTone = user.SkinTone
                        },
                        StatsInfo = new StatsInfoModel
                        {
                            Level = user.Level,
                            Exp = user.Exp,
                            Cash = user.Cash,
                            GoldenCoins = user.GoldenCoins,
                            Manager = user.Admin,
                            Officer = user.Officer
                        },
                        Loadout = dbContext.Items.FindMany(user.Hair, user.Top, user.Pants, user.Shoes, user.BodySuit,
                                                            user.Coat, user.Earings, user.Hat, user.Hovers, user.Neckless, user.Facial)
                                                                    .Select(x => new EquipedLoadoutModel(x)).ToArray()
                    };
                    return userInfo;
                }
            }
            return default;
        }
    }
}
