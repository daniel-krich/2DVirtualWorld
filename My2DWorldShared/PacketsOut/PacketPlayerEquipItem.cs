using My2DWorldShared.Enums;
using My2DWorldShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsOut
{
    public class PacketPlayerEquipItem : PacketOutBase
    {
        public string? PlayerName { get; set; }
        public EquipedLoadoutModel? Item { get; set; }
        public PacketPlayerEquipItem()
        {
            Id = OutComingPacketId.PlayerEquipItem;
        }
    }
}
