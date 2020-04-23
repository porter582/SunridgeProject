using System;
using System.IO;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Sunridge.Models;
using System.Collections.Generic;
using Sunridge.Utility;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ReportController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
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
                return Json(new { data = _unitOfWork.ReportItem.GetAll(null, null, null) });
            }
            else
            {
                return Json(new { data = _unitOfWork.ReportItem.GetAll(u => u.ApplicationUserId == claim.Value, null, null) });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var objFromDb = _unitOfWork.ReportItem.GetFirstOrDefault(u => u.Id == id);
                IEnumerable<LaborHours> laborFromDb = _unitOfWork.LaborHoursItem.GetAll(u => u.ReportId == id);
                IEnumerable<EquipmentHours> equipmentFromDb = _unitOfWork.EquipmentHoursItem.GetAll(u => u.ReportId == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                if (laborFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                if (equipmentFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                _unitOfWork.ReportItem.Remove(objFromDb);
                foreach(var item in laborFromDb)
                {
                    _unitOfWork.LaborHoursItem.Remove(item);
                }
                foreach (var item in equipmentFromDb)
                {
                    _unitOfWork.EquipmentHoursItem.Remove(item);
                }
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