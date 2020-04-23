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

namespace Sunridge.Pages.Admin.CommonAreaAssets
{
    [Authorize(Roles = SD.AdminRole)]
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
        public  Models.CommonAreaAsset CommonAreaObj { get; set; }
        public IActionResult OnGet(int? id) ///IActionResult return type is page, obj
        {

            CommonAreaObj = new Models.CommonAreaAsset();
            if (id != null) //edit
            {
                CommonAreaObj = _unitofWork.CommonAreaAsset.GetFirstOrDefault(u => u.CommonAreaAssetId == id);
                if (CommonAreaObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (CommonAreaObj.CommonAreaAssetId == 0) //new category
            {
                _unitofWork.CommonAreaAsset.Add(CommonAreaObj);
            }
            else
            {
                _unitofWork.CommonAreaAsset.Update(CommonAreaObj);
            }
            _unitofWork.Save();
            return RedirectToPage("./Index");
        }
    }
}