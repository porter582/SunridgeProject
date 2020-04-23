using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Classifieds.Services
{
    public class UpsertModel : PageModel
    {

        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UpsertModel(IUnitOfWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }

        //binds the model to the page
        [BindProperty]
        public ClassifiedService ClassifiedServiceObj { get; set; }
        public IActionResult OnGet(int? id) ///IActionResult return type is page, obj
        {

            if (id != null) //edit
            {
                ClassifiedServiceObj = _unitofWork.ClassifiedService.GetFirstOrDefault(u => u.Id == id);
                if (ClassifiedServiceObj == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                ClassifiedServiceObj.ApplicationUserId = claim.Value;


                //find root path wwwroot
                string webRootPath = _hostingEnvironment.WebRootPath;
                //Grab the file(s) from the form
                var files = HttpContext.Request.Form.Files;

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                if (ClassifiedServiceObj.Id == 0) //new lostandfounditem
                {
                    //rename file user submits for image
                    string fileName = Guid.NewGuid().ToString();
                    //upload file to the path
                    var uploads = Path.Combine(webRootPath, @"images\ClassifiedService");
                    //preserve our extension
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream); //files variable comes from the razor page files id
                    }

                    ClassifiedServiceObj.Image = @"\images\ClassifiedService\" + fileName + extension;

                    _unitofWork.ClassifiedService.Add(ClassifiedServiceObj);
                }
                else //else we edit
                {
                    var objFromDb =
                        _unitofWork.ClassifiedService.Get(ClassifiedServiceObj.Id);
                    //checks if there are files submitted
                    if (files.Count > 0)
                    {
                        //rename file user submits for image
                        string fileName = Guid.NewGuid().ToString();
                        //upload file to the path
                        var uploads = Path.Combine(webRootPath, @"images\ClassifiedService");
                        //preserve our extension
                        var extension = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var filestream = new FileStream(Path.Combine(uploads, fileName, extension), FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }

                        ClassifiedServiceObj.Image = @"\images\ClassifiedService\" + fileName + extension;
                    }
                    else
                    {
                        ClassifiedServiceObj.Image = objFromDb.Image;
                    }

                    _unitofWork.ClassifiedService.Update(ClassifiedServiceObj);
                }
            }
            _unitofWork.Save();
            return RedirectToPage("./Index");
        }
    }
}