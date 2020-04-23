using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class AdminComments
    {
        public int Id { get; set; }

        [Display(Name = "Comment")]
        public string AdminComment { get; set; }

        public int ReportId { get; set; }
        [NotMapped]
        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
