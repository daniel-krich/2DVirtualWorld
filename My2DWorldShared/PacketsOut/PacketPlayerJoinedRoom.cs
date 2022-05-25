using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My2DWorldShared.Enums;
using My2DWorldShared.Models;

namespace My2DWorldShared.PacketsOut
{
    public class PacketPlayerJoinedRoom : PacketOutBase
    {
        public UserInformationModel? Player { get; set; }
        public PacketPlayerJoinedRoom()
        {
            Id = OutComingPacketId.PlayerJoinedRoom;
        }
    }
}
