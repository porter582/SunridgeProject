using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sunridge.Models
{
    public class ClassifiedCategory
    {
        public int ClassifiedCategoryId { get; set; }
        [Display(Name = "Category")]
        public string Description { get; set; }
    }
}
