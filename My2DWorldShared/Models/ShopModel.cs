using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.Models
{
    public class ShopModel
    {
        public string? ShopName { get; set; }
        public ItemInfoModel[]? Batch { get; set; }
        public ShopBatchInfoModel? Info { get; set; }
    }

    public class ShopBatchInfoModel
    {
        public int? ItemsCount { get; set; }
        public int? MaxItemsPerBatch { get; set; }
        public int? ItemsBatchCount { get; set; }
    }
}
