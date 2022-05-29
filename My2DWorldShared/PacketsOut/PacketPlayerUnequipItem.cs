using My2DWorldShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsOut
{
    public class PacketPlayerUnequipItem : PacketOutBase
    {
        public string? PlayerName { get; set; }
        public ItemType? ItemType { get; set; }
        public PacketPlayerUnequipItem()
        {
            Id = OutComingPacketId.PlayerUnequipItem;
        }
    }
}
