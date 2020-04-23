using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Admin.HOALots
{
    public class FilesUpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        //binds the model to the page
        [BindProperty]
        public Sunridge.Models.File file { get; set; }
        [BindProperty]
        public string LotId { get; set; }

        public FilesUpsertModel(IUnitOfWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                string[] temp = (string[])TempData["lotId"];
                LotId = temp[0];
                TempData["lotId"] = LotId;
            }
            catch
            {
                string temp = TempData["lotId"].ToString();
                LotId = temp;
                TempData["lotId"] = id;
            }
            file = new Sunridge.Models.File();

            if (id != 0)
                file = _unitofWork.File.GetFirstOrDefault(u => u.FileId == id);

            return Page();
        }

        public IActionResult OnPost()
        {

            //find root path wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
            //Grab the file(s) from the form
            var files = HttpContext.Request.Form.Files;

            //checks if there are files submitted
            if (files.Count > 0)
            {
                file.FileURL = files[0].FileName;
                //rename file user submits for image
                string fileName = file.FileURL;
                //upload file to the path
                var uploads = Path.Combine(webRootPath, @"docs\lotFiles");
                //preserve our extension
                //var extension = Path.GetExtension(files[0].FileName);

                var imagePath = Path.Combine(webRootPath, files[0].FileName.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                using (var filestream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
            }
            else
            {
                return Page();
            }

            var tempFile = _unitofWork.File.GetFirstOrDefault(s => s.FileId == file.FileId);



            if (file.FileId != 0)
            {
                file.LotId = tempFile.LotId;
                _unitofWork.File.Update(file);
            }
            else
                _unitofWork.File.Add(file);

            _unitofWork.Save();

            TempData["lotId"] = file.LotId;

            return RedirectToPage($"./FilesIndex", new { id = file.Lot });
        }
    }
}