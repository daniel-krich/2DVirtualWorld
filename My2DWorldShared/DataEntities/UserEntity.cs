using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.DataEntities
{
    public class UserEntity : BaseEntity
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int Cash { get; set; }
        public int GoldenCoins { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }
        public int SkinTone { get; set; }
        public int EyeColor { get; set; }
        public int Hair { get; set; }
        public int Top { get; set; }
        public int Pants { get; set; }
        public int Shoes { get; set; }
        public int Coat { get; set; }
        public int Hat { get; set; }
        public int Facial { get; set; }
        public int Neckless { get; set; }
        public int BodySuit { get; set; }
        public int Earings { get; set; }
        public int Hovers { get; set; }
        

        public int? LastLocationId { get; set; }
        public virtual MapEntity? LastLocation { get; set; }

        public virtual ICollection<UserInventoryEntity>? Inventory { get; set; }

        public int Admin { get; set; }
        public int Officer { get; set; }
    }
}
