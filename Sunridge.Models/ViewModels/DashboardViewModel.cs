using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int DashboardViewModelId { get; set; }
        public ApplicationUser Owner { get; set; }
        public List<Lot> Lots{ get; set; }
        //public List<LotInventory> LotInventories { get; set; }
        public List<KeyHistory> KeyHistories { get; set; }
        
    }
}
