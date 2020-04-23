using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class HOALotVM
    {
        public int id { get; set; }
        public string LotNumber { get; set; }
        public string StreetAddress { get; set; }
        public string UserName { get; set; }
        public string TaxId { get; set; }
        public string LotInventory { get; set; }
        public string primaryOwnerId { get; set; }
        public string secondaryOwnerId { get; set; }
    }
}
