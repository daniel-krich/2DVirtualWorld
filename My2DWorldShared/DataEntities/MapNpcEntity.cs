using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.DataEntities
{
    public class MapNpcEntity : BaseEntity
    {
        public int MapId { get; set; }
        public virtual MapEntity? Map { get; set; }

        public int NpcId { get; set; }
        public virtual NpcEntity? Npc { get; set; }

        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
    }
}
