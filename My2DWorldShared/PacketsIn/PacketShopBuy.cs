using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsIn
{
    public class PacketShopBuy : PacketInBase
    {
        public int ShopID { get; set; }
        public int ItemID { get; set; }
    }
}
