using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.DataEntities
{
    public class ItemEntity : BaseEntity
    {
        public int Type { get; set; }
        public int PriceType { get; set; }
        public int Price { get; set; }
        public string? FilePath { get; set; }
        public string? Name { get; set; }
        public string? ItemDesc { get; set; }
    }
}
