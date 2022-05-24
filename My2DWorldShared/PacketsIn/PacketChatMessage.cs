using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsIn
{
    public class PacketChatMessage : PacketInBase
    {
        public string? Message { get; set; }
    }
}
