using Microsoft.EntityFrameworkCore;
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
        [MaxLength(64)]
        public string? Name { get; set; }
        [MaxLength(256)]
        public string? FilePath { get; set; }
    }
}
