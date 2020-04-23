using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.ScheduledEvents
{
    [Authorize(Roles = SD.AdminRole)]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateModel(IUnitOfWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }

        //binds the model to the page
        [BindProperty]
        public Models.ScheduledEvent ScheduledEventObj { get; set; }
        public IActionResult OnGet(int? id) ///IActionResult return type is page, obj
        {

            ScheduledEventObj = new Models.ScheduledEvent();
            if (id != null) //edit
            {
                ScheduledEventObj = _unitofWork.ScheduledEvents.GetFirstOrDefault(u => u.ID == id);

                if (ScheduledEventObj == null)
                {
                    return NotFound();
                }
            }
            else
            {
                ScheduledEventObj = new Models.ScheduledEvent();

            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ScheduledEventObj.ID == 0) //new category
            {
                _unitofWork.ScheduledEvents.Add(ScheduledEventObj);
            }
            else
            {
                _unitofWork.ScheduledEvents.Update(ScheduledEventObj);
            }

           
            _unitofWork.Save();
            return RedirectToPage("./Index");
        }
    }
}