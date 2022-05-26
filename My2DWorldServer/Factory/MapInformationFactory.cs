using Microsoft.EntityFrameworkCore;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
using My2DWorldShared.Enums;
using My2DWorldShared.Extensions;
using My2DWorldShared.Models;
using System.Text.Json;

namespace My2DWorldServer.Factory
{
    public class MapInformationFactory
    {
        private UsersSessionCollection _users;
        private IDbContextFactory<SqlDbContext> _dbContextFactory;
        private UserSession _session;
        public MapInformationFactory(IDbContextFactory<SqlDbContext> dbContextFactory, UserSession session, UsersSessionCollection users)
        {
            _dbContextFactory = dbContextFactory;
            _users = users;
            _session = session;
        }

        public async Task<MapInformationModel?> Create(int mapId)
        {
            using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
            {
                MapEntity? map = await dbContext.Maps.FindAsync(mapId);
                if (map != null)
                {
                    UserInformationFactory userFactory = new UserInformationFactory(_dbContextFactory, _users);
                    var mapInfo = new MapInformationModel
                    {
                        FilePath = map.FilePath,
                        Name = map.Name,
                        SpawnX = map.SpawnX,
                        SpawnY = map.SpawnY,
                        MapLeave = map.Exits?.Select(x => new MapExitInfo
                        {
                            ExitId = x.Id,
                            MapExitId = x.MapExitId,
                            MapExitName = x.MapExit?.Name,
                            ArrowAngle = x.ArrowAngle,
                            EntranceX = x.EntranceX,
                            EntranceY = x.EntranceY,
                            ExitTeleportX = x.ExitTeleportX,
                            ExitTeleportY = x.ExitTeleportY
                        }).ToArray(),
                        Npcs = map.Npcs?.Select(x => new MapNpcInfo
                        {
                            Npc = new NpcInfo
                            {
                                About = x.Npc?.About,
                                FilePath = x.Npc?.FilePath,
                                Name = x.Npc?.Name,
                                NpcId = x.Npc?.Id ?? -1,
                                Speeches = x.Npc?.Speeches?.Select(s => s.Speech).ToArray(),
                                Games = x.Npc?.Games?.Select(g => new MiniGameInfo
                                {
                                    GameId = g.GameId,
                                    GameName = g.Game?.Name,
                                    FilePath = g.Game?.FilePath,
                                }).ToArray()
                            },
                            PositionX = x.PositionX,
                            PositionY = x.PositionY,
                            ScaleX = x.ScaleX,
                            ScaleY = x.ScaleY,
                        }).ToArray(),
                        Users = (await Task.WhenAll(dbContext.Users.FindMany(_users.Sessions.Where(x => x != _session && x.ServerId == _session.ServerId && x.MapId == mapId && x.GameId == null).Select(x => x.UserId))
                            .Select(x => userFactory.Create(x)).ToArray()))
                    };


                    return mapInfo;
                }
            }
            return default;
        }
    }
}
