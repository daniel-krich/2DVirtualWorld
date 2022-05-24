using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsIn
{
    public class PacketGameProgressUpdate : PacketInBase
    {
        public int GameId { get; set; }
        public int Score { get; set; }
    }
}
