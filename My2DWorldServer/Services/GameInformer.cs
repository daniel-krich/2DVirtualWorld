using Microsoft.EntityFrameworkCore;
using My2DWorldServer.Extensions;
using My2DWorldServer.Factory;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
using My2DWorldShared.Enums;
using My2DWorldShared.Models;
using My2DWorldShared.PacketsOut;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace My2DWorldServer.Services
{
    public class GameInformer : IGameInformer
    {
        private UsersSessionCollection _users;
        private IDbContextFactory<SqlDbContext> _dbContextFactory;
        private UserSession _session;
        public GameInformer(UsersSessionCollection users, UserSession session, IDbContextFactory<SqlDbContext> dbContextFactory)
        {
            _users = users;
            _session = session;
            _dbContextFactory = dbContextFactory;
        }

        public async Task SendAuthenticateConnection()
        {
            using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
            {
                var auth = new PacketAuthenticateConnection
                {
                    Succeeded = true,
                    Servers = dbContext.Servers.ToList().Select(x => new ServerInfoModel
                    {
                        Id = x.Id,
                        Name = x.ServerName,
                        MaxPlayers = x.ServerMaxPlayers,
                        Online = _users.Sessions.Count(y => y.ServerId == x.Id)
                    }).ToArray()
                };

                await _session.WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(auth))), WebSocketMessageType.Binary, true, CancellationToken.None);
            }
        }

        public async Task SendAuthenticateRejectConnection(string reason)
        {
            var auth = new PacketAuthenticateConnection(reason);

            await _session.WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(auth))), WebSocketMessageType.Binary, true, CancellationToken.None);
        }

        public async Task SendPushUserInformation()
        {
            if (_session.Logged)
            {
                using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    UserEntity? user = await dbContext.Users.FindAsync(_session.UserId);
                    if (user != null)
                    {
                        UserInformationFactory userFactory = new UserInformationFactory(_dbContextFactory, _users);
                        var userInfo = new PacketPushUserInformation
                        {
                            Info = await userFactory.Create(user)
                        };
                        await _session.WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(userInfo))), WebSocketMessageType.Binary, true, CancellationToken.None);
                    }
                }
            }
        }

        public async Task SendMapChange(int mapId, int exitId = -1)
        {
            if (_session.Logged)
            {
                using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {

                    UserEntity? user = await dbContext.Users.FindAsync(_session.UserId);

                    await SendExitedRoomToAll(user);

                    MapInformationFactory mapFactory = new MapInformationFactory(_dbContextFactory, _session, _users);
                    PacketChangeMap mapChange = new PacketChangeMap
                    {
                        Info = await mapFactory.Create(mapId)
                    };
                    _session.MapId = mapId;
                    MapExitInfo? mapExit = mapChange.Info?.MapLeave?.FirstOrDefault(x => x.ExitId == exitId);
                    if (mapExit != null)
                    {
                        _session.Position = new PlayerPosition(mapExit.ExitTeleportX, mapExit.ExitTeleportY);
                    }
                    else
                    {
                        _session.Position = new PlayerPosition(mapChange.Info?.SpawnX ?? 0, mapChange.Info?.SpawnY ?? 0);
                    }
                    await _session.WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(mapChange))), WebSocketMessageType.Binary, true, CancellationToken.None);

                    await SendJoinedRoomToAll(user);
                }
            }
        }

        public async Task SendPlayerUpdatePosition(float x, float y)
        {
            using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
            {
                _session.Position = new PlayerPosition(x, y);
                PacketUpdatePosition packetUpdate = new PacketUpdatePosition((await dbContext.Users.FindAsync(_session.UserId))?.Username, x, y);
                _users.Sessions
                    .Where(x => x.MapId == _session.MapId && x.ServerId == _session.ServerId)
                    .ForEachCustom(x => x.WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(packetUpdate))), WebSocketMessageType.Binary, true, CancellationToken.None));
            }
        }

        public async Task SendJoinedRoomToAll(UserEntity? user)
        {
            UserInformationFactory userInfo = new UserInformationFactory(_dbContextFactory, _users);
            UserInformationModel? userInformation = await userInfo.Create(user);
            PacketPlayerJoinedRoom joinRoomPlayer = new PacketPlayerJoinedRoom
            {
                Player = userInformation
            };
            var arraySegmentSendPlayerJoinedRoom = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(joinRoomPlayer)));

            await _users.Sessions.Where(x => x != _session && x.ServerId == _session.ServerId && x.MapId == _session.MapId)
                .ForEachAsyncCustom(x => x.WebSocket.SendAsync(arraySegmentSendPlayerJoinedRoom, WebSocketMessageType.Binary, true, CancellationToken.None));
        }

        public async Task SendExitedRoomToAll(UserEntity? user)
        {
            PacketPlayerExitRoom exitRoom = new PacketPlayerExitRoom
            {
                PlayerName = user?.Username
            };

            var arraySegmentSendPlayerExitRoom = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(exitRoom)));

            await _users.Sessions.Where(x => x != _session && x.ServerId == _session.ServerId && x.MapId == _session.MapId)
                .ForEachAsyncCustom(x => x.WebSocket.SendAsync(arraySegmentSendPlayerExitRoom, WebSocketMessageType.Binary, true, CancellationToken.None));
        }
    }
}
