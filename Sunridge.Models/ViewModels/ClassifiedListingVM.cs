using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    [NotMapped]
    public class ClassifiedListingVM
    {
        public ClassifiedListing ClassifiedListing { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public List<ClassifiedImage> ImagesList { get; set; }
    }
}
