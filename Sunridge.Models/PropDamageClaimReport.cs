using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    //public class NewsItem : DbItem
    public class PropDamageClaimReport
    {
        public int id { get; set; }

        public string username { get; set; }

        [Display(Name = "Listing Date")]
        public string listingDate { get; set; }

        public string comments { get; set; }

        public bool resolved { get; set; }
        public string resolveddate { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        public string ApplicationUserId { get; set; }
        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
