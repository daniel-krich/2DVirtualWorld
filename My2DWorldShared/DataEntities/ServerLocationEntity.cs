using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.DataEntities
{
    public class ServerLocationEntity : BaseEntity
    {
        [MaxLength(32)]
        public string? ServerName { get; set; }
        public int ServerMaxPlayers { get; set; }
    }
}
