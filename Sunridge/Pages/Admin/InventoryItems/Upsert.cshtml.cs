using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Admin.InventoryItems
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        //binds the model to the page
        [BindProperty]
        public Inventory inventoryObj { get; set; }

        public UpsertModel(IUnitOfWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet(int id)
        {
            inventoryObj = _unitofWork.Inventory.GetFirstOrDefault(s => s.InventoryId == id);

            if (inventoryObj == null)
                inventoryObj = new Inventory();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (inventoryObj.InventoryId != 0)
            {
                _unitofWork.Inventory.Update(inventoryObj);
            }
            else
            {
                _unitofWork.Inventory.Add(inventoryObj);
            }

            _unitofWork.Save();

            return RedirectToPage("./Index");
        }
    }
}