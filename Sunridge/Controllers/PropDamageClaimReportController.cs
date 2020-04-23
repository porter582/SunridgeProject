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
    public class PropDamageClaimReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public PropDamageClaimReportController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (User.IsInRole(SD.AdminRole))
            {
                return Json(new { data = _unitOfWork.PropDamageClaimReport.GetAll(null, null, null) });
            }
            else
            {
                return Json(new { data = _unitOfWork.PropDamageClaimReport.GetAll(u => u.ApplicationUserId == claim.Value, null, null) });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var objFromDb = _unitOfWork.PropDamageClaimReport.GetFirstOrDefault(u => u.id == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                //Physically Delete the image in wwwroot
                //var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.FilePath.TrimStart('\\'));
                //if (System.IO.File.Exists(imagePath))
                //{
                //   System.IO.File.Delete(imagePath);
                //}

                _unitOfWork.PropDamageClaimReport.Remove(objFromDb);
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