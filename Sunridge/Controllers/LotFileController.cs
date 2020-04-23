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
    public class LotFileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public LotFileController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            int theId = 0;
            try
            {
                string[] id = (string[])TempData["lotId"];
                theId = Int32.Parse(id[0]);
                TempData["lotId"] = id;
                if (theId == 0)
                    return Json(new { data = 0 });
            }
            catch
            {
                string id = TempData["lotId"].ToString();
                theId = Int32.Parse(id);
                TempData["lotId"] = id;
                if (theId == 0)
                    return Json(new { data = 0 });
            }

            return Json(new { data = _unitOfWork.File.GetAll(s => s.LotId == theId, null, null) });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var objFromDb = _unitOfWork.File.GetFirstOrDefault(u => u.FileId == id);
                TempData["lotId"] = objFromDb.LotId;

                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                //Physically Delete the file in wwwroot
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath + "\\docs\\lotFiles\\" + objFromDb.FileURL.TrimStart('\\'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _unitOfWork.File.Remove(objFromDb);
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