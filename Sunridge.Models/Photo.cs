using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please type a title")]
        public string Title { get; set; }
        //[Required(ErrorMessage = "Please type an year")]
        public int Year { get; set; }
        
        public string Image { get; set; }

        ////Nav props
        //[ForeignKey("OwnerId")]
        //public virtual ApplicationUser Owner { get; set; }
    }
}
