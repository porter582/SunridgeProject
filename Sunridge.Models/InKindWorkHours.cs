using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class InKindWorkHours
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double? Hours { get; set; }
        public string Type { get; set; }
        public int FormResponseId { get; set; }

        [ForeignKey("FormResponseId")]
        public FormResponse FormResponse { get; set; }
    }
}
