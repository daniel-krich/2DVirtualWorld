using My2DWorldShared.Enums;
using My2DWorldShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsOut
{
    public class PacketSendShopLoadBatch : PacketOutBase
    {
        public ShopModel? Shop { get; set; }
        public PacketSendShopLoadBatch()
        {
            Id = OutComingPacketId.SendShopLoadBatch;
        }
    }
}
