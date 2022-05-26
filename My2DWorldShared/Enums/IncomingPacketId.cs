using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.Enums
{
    public enum IncomingPacketId
    {
        Authenticate,
        ChangeServer,
        EquipItem,
        PlayerMove,
        ChatMessage,
        MapChange,
        MiniGameLoad,
        MiniGameQuit,
        MiniGameProgressUpdate,
        ShopLoad,
        ShopBuy,
        RequestChangeServer
    }
}
