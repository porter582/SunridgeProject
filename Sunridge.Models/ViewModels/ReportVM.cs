using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models.ViewModels
{
    public class ReportVM
    {
        public int ReportVMId { get; set; }
        public Report Report { get; set; }
        public List<EquipmentHours> EquipmentHours { get; set; }
        public List<LaborHours> LaborHours { get; set; }
        public AdminComments AdminComments { get; set; }
    }
}
