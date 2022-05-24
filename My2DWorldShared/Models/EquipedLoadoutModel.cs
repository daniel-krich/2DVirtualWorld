using My2DWorldShared.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.Models
{
    public class EquipedLoadoutModel
    {
        public int? Id { get; set; }
        public int? Type { get; set; }
        public string? FilePath { get; set; }

        public EquipedLoadoutModel()
        {

        }

        public EquipedLoadoutModel(ItemEntity? item)
        {
            Id = item?.Id;
            Type = item?.Type;
            FilePath = item?.FilePath;
        }
    }
}
