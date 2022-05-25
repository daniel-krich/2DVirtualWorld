using My2DWorldShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsOut
{
    public class PacketPushChatMessage : PacketOutBase
    {
        public string? PlayerName { get; set; }
        public string? Message { get; set; }
        public PacketPushChatMessage()
        {
            Id = OutComingPacketId.PlayerSpeech;
        }

        public PacketPushChatMessage(string name, string message) : this()
        {
            PlayerName = name;
            Message = message;
        }
    }
}
