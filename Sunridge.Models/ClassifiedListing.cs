using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class ClassifiedListing
    {
        public int ClassifiedListingId { get; set; }
        public string OwnerId { get; set; }
        public int ClassifiedCategoryId { get; set; }
        public string Categories { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(75, MinimumLength = 5)]
        public string ItemName { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Please enter a positive value")]
        public float Price { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 25)]
        public string Description { get; set; }

        [Display(Name = "Listing Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ListingDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }

        //Nav properties
        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }
        [ForeignKey("ClassifiedCategoryId")]
        public virtual ClassifiedCategory Category { get; set; }
        [Required]
        public List<ClassifiedImage> Images { get; set; }

        public string classifiedcategory { get; set; }
    }
}
