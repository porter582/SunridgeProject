using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sunridge.Data;
using Sunridge.Models;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.ScheduledEvents
{
    [Authorize(Roles = SD.AdminRole)]
    public class DetailsModel : PageModel
    {
        private readonly Sunridge.Data.ApplicationDbContext _context;

        public DetailsModel(Sunridge.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ScheduledEvent ScheduledEvent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ScheduledEvent = await _context.ScheduledEvent.FirstOrDefaultAsync(m => m.ID == id);

            if (ScheduledEvent == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
