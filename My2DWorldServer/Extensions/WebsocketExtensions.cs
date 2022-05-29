using System.Net.WebSockets;
using System.Text;

namespace My2DWorldServer.Extensions
{
    public static class WebsocketExtensions
    {
        public static Task SendAsync(this WebSocket ws, object obj, CancellationToken token)
        {
            return ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializerExtensions.SerializeUnicode(obj))), WebSocketMessageType.Binary, true, token);
        }
    }
}
