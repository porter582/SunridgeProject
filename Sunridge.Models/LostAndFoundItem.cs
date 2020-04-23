using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class LostAndFoundItem
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; } //Must be lowercase when using .js for data
        public string Image { get; set; }
        public string ApplicationUserId { get; set; }
        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string username { get; set; }
    }
}