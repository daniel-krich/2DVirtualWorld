using My2DWorldShared.Enums;
using My2DWorldShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsOut
{
    public class PacketPlayerGameLoad : PacketOutBase
    {
        public MiniGameInfo? Game { get; set; }

        public PacketPlayerGameLoad()
        {
            Id = OutComingPacketId.PlayerGameLoad;
        }

        public PacketPlayerGameLoad(int? gameId, string? name, string? filepath) : this()
        {
            Game = new MiniGameInfo
            {
                GameId = gameId,
                GameName = name,
                FilePath = filepath
            };
        }
    }
}
