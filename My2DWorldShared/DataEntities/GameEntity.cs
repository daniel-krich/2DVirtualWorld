using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.DataEntities
{
    public class GameEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? FilePath { get; set; }
    }
}
