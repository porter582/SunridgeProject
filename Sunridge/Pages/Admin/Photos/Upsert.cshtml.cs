using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Admin.Photos
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;
        public readonly IWebHostEnvironment _hostingEnvironment;

        public UpsertModel(IUnitOfWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }

        //binds the model to the page
        [BindProperty]
        public  AdminPhotoViewModels PhotosObj { get; set; }
        public IActionResult OnGet(int? id) ///IActionResult return type is page, obj
        {
            //LostAndFoundItem = new LostAndFoundItem();

            if (id != null) //edit
            {
                PhotosObj = new AdminPhotoViewModels();
                PhotosObj.Photo = _unitofWork.Photo.GetFirstOrDefault(u => u.Id == id);
                PhotosObj.Categories = _unitofWork.PhotoCategories.GetPhotoCategoriesListOrDropdown();

                if (PhotosObj == null)
                {
                    return NotFound();
                }
            } else
            {
                PhotosObj = new AdminPhotoViewModels();
                PhotosObj.Photo = new Photo();
                PhotosObj.Categories = _unitofWork.PhotoCategories.GetPhotoCategoriesListOrDropdown();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _unitofWork.ApplicationUser.GetFirstOrDefault(s => s.Id == userId);
            PhotosObj.Photo.Name = user.FirstName + " " + user.LastName;

            //find root path wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
            //Grab the file(s) from the form
            var files = HttpContext.Request.Form.Files;

            if (PhotosObj.Photo.Id == 0) //new photo object
            {
                //rename file user submits for image
                string fileName = Guid.NewGuid().ToString();
                //upload file to the path
                var uploads = Path.Combine(webRootPath, @"img\photo-gal");
                //preserve our extension
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream); //files variable comes from the razor page files id
                }

                PhotosObj.Photo.Image = @"\img\photo-gal\" + fileName + extension;

                _unitofWork.Photo.Add(PhotosObj.Photo);
            }
            else //else we edit
            {
                var objFromDb =
                    _unitofWork.Photo.Get(PhotosObj.Photo.Id);
                //checks if there are files submitted
                if (files.Count > 0)
                {
                    //rename file user submits for image
                    string fileName = Guid.NewGuid().ToString();
                    //upload file to the path
                    var uploads = Path.Combine(webRootPath, @"img\photo-gal");
                    //preserve our extension
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }

                    PhotosObj.Photo.Image = @"\img\photo-gal\" + fileName + extension;
                }
                else
                {
                    PhotosObj.Photo.Image = objFromDb.Image;
                }
                _unitofWork.Photo.Update(PhotosObj.Photo);
            }
            _unitofWork.Save();
            return RedirectToPage("./Index");
        }
    }
}