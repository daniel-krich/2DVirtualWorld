using My2DWorldShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.Models
{
    public class InventoryItemModel
    {
        public int? ItemId { get; set; }
        public int? Type { get; set; }
        public PriceType? PriceType { get; set; }
        public int? Price { get; set; }
        public int? ItemQuantity { get; set; }
        public string? FilePath { get; set; }
        public string? Name { get; set; }
        public string? ItemDesc { get; set; }
    }
}
