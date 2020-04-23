using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;
using Sunridge.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.Models;
using System.Web;

namespace Sunridge.Pages.Admin.BannerItems
{
    [Authorize(Roles = SD.AdminRole)]//have to be admin to access this page
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        //dependency injection
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Banner BannerObj { get; set; }

        public IActionResult OnGet(int? id)  //id is optional
        {
            //populate the lists for dropdowns
            BannerObj = new Banner();

            if (id != null) //edit
            {
                BannerObj = _unitOfWork.Banner.GetFirstOrDefault(u => u.Id == id);
                if (BannerObj == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            //find root path to wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
            //grab the file(s) from the form
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }


            //new menu item
            if (BannerObj.Id == 0)
            {
                //rename the file the user submits
                //guid is a unique string that will not be duplicated
                string fileName = Guid.NewGuid().ToString();

                //upload to path
                var uploads = Path.Combine(webRootPath, @"images\banner");

                //preserve our extension
                var extension = Path.GetExtension(files[0].FileName);

                //append new name to the extension
                using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }

                BannerObj.Image = @"\images\banner\" + fileName + extension;

                //convert to html
                HttpUtility.HtmlEncode(BannerObj.Body);
 

                //add it to the db
                _unitOfWork.Banner.Add(BannerObj);
            }
            //edit the menu item 
            else
            {
                var objFromDb = _unitOfWork.Banner.Get(BannerObj.Id);
                //were there any files
                if (files.Count > 0)
                {
                    //rename the file the user submits
                    //guid is a unique string that will not be duplicated
                    string fileName = Guid.NewGuid().ToString();

                    //upload to path
                    var uploads = Path.Combine(webRootPath, @"images\banner");

                    //preserve our extension
                    var extension = Path.GetExtension(files[0].FileName);
                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    //if image already exists
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    //append new name to the extension
                    using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }

                    BannerObj.Image = @"\images\banner\" + fileName + extension;
                }
                else
                {
                    BannerObj.Image = objFromDb.Image;
                }

                _unitOfWork.Banner.Update(BannerObj);
            }

            //commit changes to the db
            _unitOfWork.Save();

            return RedirectToPage("./Index");
        }
    }
}