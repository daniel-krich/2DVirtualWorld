using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsIn
{
    public class PacketMapChange : PacketInBase
    {
        public int MapId { get; set; }
        public int ExitId { get; set; }
    }
}
