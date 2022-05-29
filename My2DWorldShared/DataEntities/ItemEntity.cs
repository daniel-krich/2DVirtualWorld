using My2DWorldShared.Enums;
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
        public ItemType Type { get; set; }
        public PriceType PriceType { get; set; }
        public int Price { get; set; }
        [MaxLength(256)]
        public string? FilePath { get; set; }
        [MaxLength(64)]
        public string? Name { get; set; }
        [MaxLength(64)]
        public string? ItemDesc { get; set; }
    }
}
