using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class LotInventory
    {
        public int LotInventoryId { get; set; }
        public int LotId { get; set; }
        public int InventoryId { get; set; }

        public string Description { get; set; }
        public bool IsArchive { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        //Nav props
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }
        [ForeignKey("InventoryId")]
        public virtual Inventory Inventory { get; set; }

    }
}
