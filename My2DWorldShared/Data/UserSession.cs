using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.Data
{
#nullable disable
    public struct PlayerPosition
    {
        public float X { get; set; }
        public float Y { get; set; }

        public PlayerPosition()
        {
            X = 0;
            Y = 0;
        }

        public PlayerPosition(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public class UserSession
    {
        public WebSocket WebSocket { get; set; }
        public int? UserId { get; set; }
        public int? ServerId { get; set; }
        public int? MapId { get; set; }
        public int? GameId { get; set; }
        public PlayerPosition Position { get; set; }
        public bool Logged { get; set; }
    }
}
