using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.DataEntities
{
    public class NpcEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? About { get; set; }
        public string? FilePath { get; set; }

        public virtual ICollection<MapNpcEntity>? Placements { get; set; }
        public virtual ICollection<NpcSpeechEntity>? Speeches { get; set; }
        public virtual ICollection<NpcGameEntity>? Games { get; set; }
    }
}
