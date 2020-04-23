using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public string Description { get; set; }
        [Display(Name="Archive")]
        public bool IsArchive { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Display(Name = "Last Modified Date")]
        public DateTime LastModifiedDate { get; set; }

        //Nav props
        public virtual ICollection<LotInventory> LotInventories { get; set; }
    }
}
