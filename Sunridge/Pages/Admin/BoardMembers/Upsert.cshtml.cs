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

namespace Sunridge.Pages.Admin.BoardMembers
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

        //all the fields on this page are bound to the menu item model
        //bind to a view model that uses all models needed for this page
        [BindProperty]
        public BoardMemberVM BoardMemberObj { get; set; }

        public IActionResult OnGet(int? id)  //id is optional
        {
            //populate the lists for dropdowns
            BoardMemberObj = new BoardMemberVM()
            {
                ApplicationUserList = _unitOfWork.ApplicationUser.GetApplicationUserListOrDropdown(),
                BoardMember = new Models.BoardMember()
            };

            if (id != null) //edit
            {
                BoardMemberObj.BoardMember = _unitOfWork.BoardMember.GetFirstOrDefault(u => u.Id == id);
                if (BoardMemberObj == null)
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
            if (BoardMemberObj.BoardMember.Id == 0)
            {
                //rename the file the user submits
                //guid is a unique string that will not be duplicated
                string fileName = Guid.NewGuid().ToString();

                //upload to path
                var uploads = Path.Combine(webRootPath, @"images\boardMembers");

                //preserve our extension
                var extension = Path.GetExtension(files[0].FileName);

                //append new name to the extension
                using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }

                BoardMemberObj.BoardMember.Image = @"\images\boardMembers\" + fileName + extension;

                //add it to the db
                _unitOfWork.BoardMember.Add(BoardMemberObj.BoardMember);
            }
            //edit the menu item 
            else
            {
                var objFromDb = _unitOfWork.BoardMember.Get(BoardMemberObj.BoardMember.Id);
                //were there any files
                if (files.Count > 0)
                {
                    //rename the file the user submits
                    //guid is a unique string that will not be duplicated
                    string fileName = Guid.NewGuid().ToString();

                    //upload to path
                    var uploads = Path.Combine(webRootPath, @"images\boardMembers");

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

                    BoardMemberObj.BoardMember.Image = @"\images\boardMembers\" + fileName + extension;
                }
                else
                {
                    BoardMemberObj.BoardMember.Image = objFromDb.Image;
                }

                _unitOfWork.BoardMember.Update(BoardMemberObj.BoardMember);
            }

            //commit changes to the db
            _unitOfWork.Save();

            return RedirectToPage("./BoardMemberEdit");
        }
    }
}