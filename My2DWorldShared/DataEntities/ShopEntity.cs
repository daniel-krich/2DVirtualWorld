using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.DataEntities
{
    public class ShopEntity : BaseEntity
    {
        [MaxLength(64)]
        public string? ShopName { get; set; }
        public virtual ICollection<ShopItemEntity>? ShopItems { get; set; }
    }
}
