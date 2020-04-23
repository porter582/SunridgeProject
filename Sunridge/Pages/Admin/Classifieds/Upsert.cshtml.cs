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
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Classifieds
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
        public ClassifiedListingVM ClassifiedObj { get; set; }

        public IActionResult OnGet(int? id) ///IActionResult return type is page, obj
        {
            //LostAndFoundItem = new LostAndFoundItem();

            if (id != null) //edit
            {
                ClassifiedObj = new ClassifiedListingVM();
                ClassifiedObj.ClassifiedListing = _unitofWork.ClassifiedListing.GetFirstOrDefault(u => u.ClassifiedListingId == id);
                ClassifiedObj.CategoryList = _unitofWork.ClassifiedCategory.GetClassifiedCategoryListOrDropdown();

                if (ClassifiedObj == null)
                {
                    return NotFound();
                }
            }
            else
            {
                ClassifiedObj = new ClassifiedListingVM();
                ClassifiedObj.ClassifiedListing = new ClassifiedListing();
                ClassifiedObj.CategoryList = _unitofWork.ClassifiedCategory.GetClassifiedCategoryListOrDropdown();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            int catNum = Int32.Parse(ClassifiedObj.ClassifiedListing.Categories);
            ClassifiedObj.ClassifiedListing.Category = _unitofWork.ClassifiedCategory.GetFirstOrDefault(u => u.ClassifiedCategoryId == catNum);
            ClassifiedObj.ClassifiedListing.classifiedcategory = ClassifiedObj.ClassifiedListing.Category.Description;
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _unitofWork.ApplicationUser.GetFirstOrDefault(s => s.Id == userId);
            // ClassifiedObj.ClassifiedListing. = user.FirstName + " " + user.LastName;

            //find root path wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
            //Grab the file(s) from the form
            var files = HttpContext.Request.Form.Files;

            if (ClassifiedObj.ClassifiedListing.ClassifiedListingId == 0)
            {
                _unitofWork.ClassifiedListing.Add(ClassifiedObj.ClassifiedListing);
                _unitofWork.Save();

                int classifiedListingId = 0;

                IEnumerable<ClassifiedListing> DBClassified = _unitofWork.ClassifiedListing.GetAll();

                foreach (var listing in DBClassified)
                {

                    if (classifiedListingId < listing.ClassifiedListingId)
                    {
                        classifiedListingId = listing.ClassifiedListingId;
                    }
                }
                for (int i = 0; i < files.Count(); i++)
                {
                    ClassifiedImage tempImage = new ClassifiedImage();
                    //rename file user submits for image
                    string fileName = Guid.NewGuid().ToString();
                    //upload file to the path
                    var uploads = Path.Combine(webRootPath, @"images\classifieds\");
                    //preserve our extension
                    var extension = Path.GetExtension(files[i].FileName);

                    using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[i].CopyTo(filestream); //files variable comes from the razor page files id
                    }

                    ClassifiedObj.ClassifiedListing.Image = @"\images\classifieds\" + fileName + extension;
                    tempImage.ImageURL = ClassifiedObj.ClassifiedListing.Image;
                    tempImage.ClassifiedListingId = classifiedListingId;
                    if (i == 0)
                    {
                        tempImage.IsMainImage = true;
                    }
                    _unitofWork.ClassifiedImage.Add(tempImage);
                }
            }
            else //else we edit
            {
                var objFromDb =
                    _unitofWork.ClassifiedListing.Get(ClassifiedObj.ClassifiedListing.ClassifiedListingId);
                //checks if there are files submitted
                if (files.Count > 0)
                {
                    List<ClassifiedImage> classifiedImages = _unitofWork.ClassifiedImage.GetAll().Where(u => u.ClassifiedListingId == ClassifiedObj.ClassifiedListing.ClassifiedListingId).ToList();
                    for (int i = 0; i < files.Count(); i++)
                    {
                        //ClassifiedImage tempImage = new ClassifiedImage();
                        //rename file user submits for image
                        string fileName = Guid.NewGuid().ToString();
                        //upload file to the path
                        var uploads = Path.Combine(webRootPath, @"images\classifieds\");
                        //preserve our extension
                        var extension = Path.GetExtension(files[i].FileName);

                        var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }
                        ClassifiedObj.ClassifiedListing.Image = @"\images\classifieds\" + fileName + extension;

                        classifiedImages.ElementAt(i).ImageURL = ClassifiedObj.ClassifiedListing.Image;
                        //if (i == 0)
                        //{
                        //    tempImage.IsMainImage = true;
                        //}
                        //ClassifiedObj.ImagesList.Add(tempImage);
                        _unitofWork.ClassifiedImage.Update(classifiedImages.ElementAt(i));
                    }
                }
                else
                {
                    ClassifiedObj.ClassifiedListing.Image = objFromDb.Image;
                }
                _unitofWork.ClassifiedListing.Update(ClassifiedObj.ClassifiedListing);
            }
            _unitofWork.Save();
            return RedirectToPage("./Index");
        }
    }
}