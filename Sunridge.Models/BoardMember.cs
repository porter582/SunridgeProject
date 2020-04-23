using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class BoardMember
    {
        public int Id { get; set; }

        public string BoardRole { get; set; }
        public string Image { get; set; }

        //keep track of who the user is
        public string ApplicationUserId { get; set; }
        
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //need order to display on page
        [Display(Name = "Display  Order")]
        public int DisplayOrder { get; set; }

    }
}
