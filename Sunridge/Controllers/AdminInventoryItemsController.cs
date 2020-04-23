using System;
using System.IO;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunridge.Utility;
using System.Security.Claims;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminInventoryItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdminInventoryItemsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Inventory.GetAll(null, null, null) });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var objFromDb = _unitOfWork.Inventory.GetFirstOrDefault(u => u.InventoryId == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                _unitOfWork.Inventory.Remove(objFromDb);
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            return Json(new { success = true, message = "Delete Successful" });

        }
    }
}