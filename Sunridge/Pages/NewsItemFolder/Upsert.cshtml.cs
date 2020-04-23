using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.NewsItemFolder
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
        public Models.NewsItem NewsItemObj { get; set; }
        public IActionResult OnGet(int? id) ///IActionResult return type is page, obj
        {
            if (id != null) //edit
            {
                NewsItemObj = _unitofWork.NewsItem.GetFirstOrDefault(u => u.NewsItemId == id);
                if (NewsItemObj == null)
                {
                    return NotFound();
                }
            }
            else
            {
                NewsItemObj = new Models.NewsItem();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            //find root path wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
            //Grab the file(s) from the form
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NewsItemObj.NewsItemId == 0) //new item
            {
                try
                {
                    //rename file user submits for image
                    string fileName = Guid.NewGuid().ToString();
                    NewsItemObj.FileName = files[0].FileName;
                    //upload file to the path
                    var uploads = Path.Combine(webRootPath, @"images\newsItems");
                    //preserve our extension

                    if (files.Count > 0)
                    {
                        var extension = Path.GetExtension(files[0].FileName);

                        using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(filestream); //files variable comes from the razor page files id
                        }

                        NewsItemObj.FilePath = @"\images\newsItems\" + fileName + extension;
                    }
                }
                catch(Exception e)
                {

                }

                _unitofWork.NewsItem.Add(NewsItemObj);
            }
            else //else we edit
            {
                var objFromDb =
                    _unitofWork.NewsItem.Get(NewsItemObj.NewsItemId);
                //checks if there are files submitted
                if (files.Count > 0)
                {
                    //rename the file the user submits
                    //guid is a unique string that will not be duplicated
                    string fileName = Guid.NewGuid().ToString();

                    //upload to path
                    var uploads = Path.Combine(webRootPath, @"images\newsItems");

                    //preserve our extension
                    var extension = Path.GetExtension(files[0].FileName);
                    var imagePath = Path.Combine(webRootPath, objFromDb.FilePath.TrimStart('\\'));

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

                    NewsItemObj.FilePath = @"\images\newsItems\" + fileName + extension;
                }
                else
                {
                    NewsItemObj.FilePath = objFromDb.FilePath;
                }
                _unitofWork.NewsItem.Update(NewsItemObj);
            }
            _unitofWork.Save();
            return RedirectToPage("./Index");
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //if (NewsItemObj.NewsItemId == 0) //new category
            //{
            //    _unitofWork.NewsItem.Add(NewsItemObj);
            //}
            //else //else we edit
            //{
            //    _unitofWork.NewsItem.Update(NewsItemObj);
            //}
            //_unitofWork.Save();
            //return RedirectToPage("./Index");
        }
    }
}