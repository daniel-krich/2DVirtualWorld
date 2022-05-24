using Microsoft.EntityFrameworkCore;
using My2DWorldServer.Services;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
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
                    if (server != null)
                    {
                        if (_users.Sessions.Count(y => y.ServerId == server.Id) < server.ServerMaxPlayers)
                        {
                            _session.ServerId = server.Id;
                            await _gameInformer.SendPushUserInformation();
                            await _gameInformer.SendMapChange(1);
                        }
                    }
                    else throw new ApplicationException("Invalid Server Id.");
                }
            }
            else throw new ApplicationException("Not authenticated.");
        }

        public Task OnChatMessage(PacketChatMessage packet)
        {
            throw new NotImplementedException();
        }

        public Task OnEquipItem(PacketEquipItem packet)
        {
            throw new NotImplementedException();
        }

        public Task OnGameLoad(PacketGameLoad packet)
        {
            throw new NotImplementedException();
        }

        public Task OnGameProgressUpdate(PacketGameProgressUpdate packet)
        {
            throw new NotImplementedException();
        }

        public Task OnGameQuit(PacketGameQuit packet)
        {
            throw new NotImplementedException();
        }

        public Task OnMapChange(PacketMapChange packet)
        {
            throw new NotImplementedException();
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
    }
}
