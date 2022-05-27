using Microsoft.EntityFrameworkCore;
using My2DWorldServer.Extensions;
using My2DWorldServer.Services;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
using My2DWorldShared.Extensions;
using My2DWorldShared.PacketsIn;
using My2DWorldShared.PacketsOut;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace My2DWorldServer.Calls
{
    public class GameCaller : IGameCaller
    {
        private UsersSessionCollection _users;
        private IDbContextFactory<SqlDbContext> _dbContextFactory;
        private UserSession _session;
        private IGameInformer _gameInformer;
        public GameCaller(UsersSessionCollection users, UserSession session, IDbContextFactory<SqlDbContext> dbContextFactory, IGameInformer gameInformer)
        {
            _users = users;
            _session = session;
            _dbContextFactory = dbContextFactory;
            _gameInformer = gameInformer;
        }

        public async Task OnAuthenticate(PacketAuthenticate packet)
        {
            if (!_session.Logged)
            {
                using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    UserEntity? user = dbContext.Users.FirstOrDefault(x => x.Username == packet.Username && x.Password == packet.Password);
                    if (user != null)
                    {
                        if (!_users.Sessions.Any(x => x.UserId == user.Id))
                        {
                            _session.Logged = true;
                            _session.UserId = user.Id;
                            await _gameInformer.SendAuthenticateConnection();
                        }
                        else
                        {
                            await _gameInformer.SendAuthenticateRejectConnection("User already connected.");
                            throw new ApplicationException("User already connected.");
                        }
                    }
                    else
                    {
                        await _gameInformer.SendAuthenticateRejectConnection("Invalid username or password.");
                        throw new ApplicationException("Invalid username or password.");
                    }
                }
            }
            else
            {
                await _gameInformer.SendAuthenticateRejectConnection("Already authenticated.");
                throw new ApplicationException("Already authenticated.");
            }
        }

        public async Task OnChangeServer(PacketChangeServer packet)
        {
            if(_session.Logged)
            {
                using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    ServerLocationEntity? server = await dbContext.Servers.FindAsync(packet.ServerId);
                    UserEntity? user = await dbContext.Users.FindAsync(_session.UserId);
                    if (server != null)
                    {
                        if (_users.Sessions.Count(y => y.ServerId == server.Id) < server.ServerMaxPlayers)
                        {
                            _session.ServerId = server.Id;
                            await _gameInformer.SendPushUserInformation();
                            await _gameInformer.SendMapChange(user?.LastLocationId ?? 1, 0);
                        }
                    }
                    else throw new ApplicationException("Invalid Server Id.");
                }
            }
            else throw new ApplicationException("Not authenticated.");
        }

        public async Task OnChatMessage(PacketChatMessage packet)
        {
            if(_session.Logged && _session.ServerId != null)
            {
                using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    UserEntity? user = await dbContext.Users.FindAsync(_session.UserId);
                    await _gameInformer.SendPushUserMessageToRoom(user, packet.Message);
                }
            }
        }

        public Task OnEquipItem(PacketEquipItem packet)
        {
            throw new NotImplementedException();
        }

        public async Task OnGameLoad(PacketGameLoad packet)
        {
            if (_session.Logged && _session.ServerId != null)
            {
                using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    GameEntity? game = await dbContext.Games.FindAsync(packet.GameId);
                    if (game != null)
                    {
                        UserEntity? user = await dbContext.Users.FindAsync(_session.UserId);
                        await _gameInformer.SendExitedRoomToAll(user);
                        _session.GameId = packet.GameId;
                        await _gameInformer.SendPlayerGameLoad(game);
                    }
                }
            }
        }

        public Task OnGameProgressUpdate(PacketGameProgressUpdate packet)
        {
            return Task.CompletedTask;
        }

        public async Task OnGameQuit(PacketGameQuit packet)
        {
            if (_session.Logged && _session.ServerId != null && _session.GameId != null)
            {
                _session.GameId = null;
                await _gameInformer.SendMapChange(_session.MapId ?? 1, -1);
            }
        }

        public Task OnMapChange(PacketMapChange packet)
        {
            if (_session.Logged && _session.ServerId != null)
            {
                _gameInformer.SendMapChange(packet.MapId, packet.ExitId);
            }
            return Task.CompletedTask;
        }

        public Task OnPlayerMove(PacketPlayerMove packet)
        {
            if (_session.Logged && _session.ServerId != null)
            {
                _gameInformer.SendPlayerUpdatePosition(packet.X, packet.Y);
            }
            return Task.CompletedTask;
        }

        public Task OnShopBuy(PacketShopBuy packet)
        {
            throw new NotImplementedException();
        }

        public Task OnShopLoad(PacketShopLoad packet)
        {
            throw new NotImplementedException();
        }

        public async Task OnRequestChangeServer(PacketRequestChangeServer packet)
        {
            if(_session.Logged && _session.ServerId != null)
            {
                await OnQuitServer();
                await _gameInformer.SendAuthenticateConnection();
            }
        }

        public async Task OnQuitServer()
        {
            if (_session.Logged && _session.MapId != null && _session.ServerId != null)
            {
                using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    UserEntity? user = await dbContext.Users.FindAsync(_session.UserId);
                    if (user != null)
                    {
                        user.LastLocationId = _session.MapId;
                        await dbContext.UpdateFieldsAsync(user, x => x.LastLocationId);
                    }
                    await _gameInformer.SendExitedRoomToAll(user);
                    _session.ServerId = null;
                    _session.MapId = null;
                    _session.GameId = null;
                }
            }
        }

        public async Task OnRequestInventoryBatch(PacketRequestInventoryBatch packet)
        {
            await _gameInformer.SendInventoryBatch(packet.Offset, 20);
        }
    }
}
