using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.DataEntities
{
    public class MapExitEntity : BaseEntity
    {
        public int MapId { get; set; }
        public virtual MapEntity? Map { get; set; }

        public int MapExitId { get; set; }
        public virtual MapEntity? MapExit { get; set; }

        public float ArrowAngle { get; set; }
        public float EntranceX { get; set; }
        public float EntranceY { get; set; }
        public float ExitTeleportX { get; set; }
        public float ExitTeleportY { get; set; }
    }
}
