using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class BoardMemberVM
    {
        public BoardMember BoardMember { get; set; }
        public IEnumerable<SelectListItem> ApplicationUserList { get; set; }

    }
}
