using Microsoft.AspNetCore.Mvc;
using My2DWorldServer.Calls;
using My2DWorldShared.Enums;
using My2DWorldShared.Data;
using My2DWorldShared.PacketsIn;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using My2DWorldServer.Services;

namespace My2DWorldServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WsController : ControllerBase
    {
        private IGameCaller _gameCaller;
        private UsersSessionCollection _users;
        private UserSession _session;
        public WsController(IGameCaller gameCaller, UsersSessionCollection users, UserSession session)
        {
            _gameCaller = gameCaller;
            _users = users;
            _session = session;
        }

        [HttpGet("connect")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {          
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _session.WebSocket = webSocket;
                _users.Sessions.Add(_session);
                await ProcessRequest(HttpContext, webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task ProcessRequest(HttpContext httpContext, WebSocket webSocket)
        {
            try
            {
                byte[] buffer = new byte[1024 * 4];
                WebSocketReceiveResult wsRes = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                while (!wsRes.CloseStatus.HasValue)
                {
                    using MemoryStream bufferStream = new MemoryStream(buffer, 0, wsRes.Count);
                    PacketInBase? packet = await JsonSerializer.DeserializeAsync<PacketInBase>(bufferStream);
                    bufferStream.Position = 0;
                    switch (packet?.Id)
                    {
                        case IncomingPacketId.Authenticate:
                            var auth = await JsonSerializer.DeserializeAsync<PacketAuthenticate>(bufferStream);
                            if(auth != null)
                                await _gameCaller.OnAuthenticate(auth);
                            break;
                        case IncomingPacketId.ChangeServer:
                            var changeserver = await JsonSerializer.DeserializeAsync<PacketChangeServer>(bufferStream);
                            if (changeserver != null)
                                await _gameCaller.OnChangeServer(changeserver);
                            break;
                        case IncomingPacketId.ChatMessage:
                            var chatmessage = await JsonSerializer.DeserializeAsync<PacketChatMessage>(bufferStream);
                            if (chatmessage != null)
                                await _gameCaller.OnChatMessage(chatmessage);
                            break;
                        case IncomingPacketId.EquipItem:
                            var equipitem = await JsonSerializer.DeserializeAsync<PacketEquipItem>(bufferStream);
                            if (equipitem != null)
                                await _gameCaller.OnEquipItem(equipitem);
                            break;
                        case IncomingPacketId.MiniGameLoad:
                            var gameload = await JsonSerializer.DeserializeAsync<PacketGameLoad>(bufferStream);
                            if (gameload != null)
                                await _gameCaller.OnGameLoad(gameload);
                            break;
                        case IncomingPacketId.MiniGameProgressUpdate:
                            var updgameprog = await JsonSerializer.DeserializeAsync<PacketGameProgressUpdate>(bufferStream);
                            if (updgameprog != null)
                                await _gameCaller.OnGameProgressUpdate(updgameprog);
                            break;
                        case IncomingPacketId.MiniGameQuit:
                            var gamequit = await JsonSerializer.DeserializeAsync<PacketGameQuit>(bufferStream);
                            if (gamequit != null)
                                await _gameCaller.OnGameQuit(gamequit);
                            break;
                        case IncomingPacketId.MapChange:
                            var mapchange = await JsonSerializer.DeserializeAsync<PacketMapChange>(bufferStream);
                            if (mapchange != null)
                                await _gameCaller.OnMapChange(mapchange);
                            break;
                        case IncomingPacketId.PlayerMove:
                            var playermove = await JsonSerializer.DeserializeAsync<PacketPlayerMove>(bufferStream);
                            if (playermove != null)
                                await _gameCaller.OnPlayerMove(playermove);
                            break;
                        case IncomingPacketId.ShopBuy:
                            var shopbuy = await JsonSerializer.DeserializeAsync<PacketShopBuy>(bufferStream);
                            if (shopbuy != null)
                                await _gameCaller.OnShopBuy(shopbuy);
                            break;
                        case IncomingPacketId.ShopLoad:
                            var shopload = await JsonSerializer.DeserializeAsync<PacketShopLoad>(bufferStream);
                            if (shopload != null)
                                await _gameCaller.OnShopLoad(shopload);
                            break;
                        case IncomingPacketId.RequestChangeServer:
                            var requestChangeServer = await JsonSerializer.DeserializeAsync<PacketRequestChangeServer>(bufferStream);
                            if (requestChangeServer != null)
                                await _gameCaller.OnRequestChangeServer(requestChangeServer);
                            break;
                        default:
                            throw new ApplicationException($"Invalid packet id sent ({packet?.Id})");
                    }
                    wsRes = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
            }
            catch
            {
                if (webSocket.State == WebSocketState.Connecting || webSocket.State == WebSocketState.Open)
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            finally
            {
                try
                {
                    await _gameCaller.OnQuitServer();
                }
                catch { }
                finally
                {
                    _users.Sessions.Remove(_session);
                }
            }
        }
    }
}
