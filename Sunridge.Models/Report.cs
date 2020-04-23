using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class Report
    {
        public int Id { get; set; }

        [Display(Name = "Form Type")]
        [Required]
        public string FormType { get; set; }
        public string ApplicationUserId { get; set; }
        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Display(Name= "Listing Date")]
        public string ListingDate { get; set; }
        [Display(Name = "Resolved Date")]
        public string ResolvedDate { get; set; }
        public string Description { get; set; }
        public string Suggestion { get; set; }
        public string Comments { get; set; }
        public bool Resolved { get; set; }

        //Nav properties
        [NotMapped]
        [ForeignKey("LaborHoursId")]
        public LaborHours LaborHoursId { get; set; }
        [NotMapped]
        [ForeignKey("EquipmentHoursId")]
        public EquipmentHours EquipmentHoursId { get; set; }

        public string username { get; set; }
    }
}