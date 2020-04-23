using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models.ViewModels
{
    [NotMapped]
    public class AdminPhotoViewModels
    {
        public Photo Photo { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
