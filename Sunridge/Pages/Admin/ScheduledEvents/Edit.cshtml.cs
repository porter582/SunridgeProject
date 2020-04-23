using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sunridge.Data;
using Sunridge.Models;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.ScheduledEvents
{
    [Authorize(Roles = SD.AdminRole)]
    public class EditModel : PageModel
    {
        private readonly Sunridge.Data.ApplicationDbContext _context;

        public EditModel(Sunridge.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ScheduledEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduledEventExists(ScheduledEvent.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScheduledEventExists(int id)
        {
            return _context.ScheduledEvent.Any(e => e.ID == id);
        }
    }
}
