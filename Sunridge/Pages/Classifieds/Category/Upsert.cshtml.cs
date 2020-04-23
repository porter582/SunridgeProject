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

namespace Sunridge.Pages.Classifieds.Category
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }
        [BindProperty]
        public Models.ClassifiedCategory ClassifiedCategoryObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            ClassifiedCategoryObj = new Models.ClassifiedCategory();
            if (id != null) //edit
            {
                ClassifiedCategoryObj = _unitofWork.ClassifiedCategory.GetFirstOrDefault(u => u.ClassifiedCategoryId == id);
                if (ClassifiedCategoryObj == null)
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
            if (ClassifiedCategoryObj.ClassifiedCategoryId == 0) //new category
            {
                _unitofWork.ClassifiedCategory.Add(ClassifiedCategoryObj);
            }
            else
            {
                _unitofWork.ClassifiedCategory.Update(ClassifiedCategoryObj);
            }
            _unitofWork.Save();
            return RedirectToPage("./Upsert");
        }
    }
}