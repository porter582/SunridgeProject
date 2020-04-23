using System;
using System.IO;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunridge.Utility;
using System.Security.Claims;
using System.Collections.Generic;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerIndexController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public OwnerIndexController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public List<OwnerLotVM> owners { get; set; }

        [HttpGet]
        public IActionResult Get()
        {
            owners = new List<OwnerLotVM>();

            if (User.IsInRole(SD.AdminRole))
            {
                var activeOwners = _unitOfWork.ApplicationUser.GetAll(s => s.IsArchive == false, null, null);
                var allLots = _unitOfWork.Lot.GetAll(s => s.IsArchive == false, null, null);              

                foreach (var owner in activeOwners)
                {     
                    OwnerLotVM temp = new OwnerLotVM();
                    temp.id = owner.Id;
                    temp.name = owner.FullName;
                    temp.username = owner.UserName;
                    temp.emergencyContactName = owner.EmergencyContactName == null ? "" : owner.EmergencyContactName;
                    temp.emergencyContactPhone = owner.EmergencyContactPhone == null ? "" : owner.EmergencyContactPhone;

                    string theLots = "";
                    var ownerLots = _unitOfWork.OwnerLot.GetAll(s => s.OwnerId == owner.Id, null, null);
                    foreach(var ol in ownerLots)
                    {
                        var lot = _unitOfWork.Lot.GetFirstOrDefault(s => s.LotId == ol.LotId);
                        theLots += lot.LotNumber + ", ";
                    }

                    if (theLots.Length > 1)
                        theLots = theLots.Substring(0, theLots.Length - 2);

                    temp.lots = theLots == null ? "" : theLots;

                    owners.Add(temp);
                }

                return Json(new { data = owners });
            }
            else //(the User.IsInRole(SD.OwnerRole))
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var owner = _unitOfWork.ApplicationUser.GetAll(s => s.Id == userId, null, null);

                foreach (var own in owner)
                {
                    OwnerLotVM temp = new OwnerLotVM();
                    temp.id = own.Id;
                    temp.name = own.FullName;
                    temp.username = own.UserName;
                    temp.emergencyContactName = own.EmergencyContactName == null ? "" : own.EmergencyContactName;
                    temp.emergencyContactPhone = own.EmergencyContactPhone == null ? "" : own.EmergencyContactPhone;

                    string theLots = "";
                    var ownerLots = _unitOfWork.OwnerLot.GetAll(s => s.OwnerId == own.Id, null, null);
                    foreach (var ol in ownerLots)
                    {
                        var lot = _unitOfWork.Lot.GetFirstOrDefault(s => s.LotId == ol.LotId);
                        theLots += lot.LotNumber + ", ";
                    }

                    if (theLots.Length > 1)
                        theLots = theLots.Substring(0, theLots.Length - 2);

                    temp.lots = theLots == null ? "" : theLots;

                    owners.Add(temp);
                }

                return Json(new { data = owners });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var objFromDb = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                objFromDb.IsArchive = true;
                _unitOfWork.ApplicationUser.Update(objFromDb);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            return Json(new { success = true, message = "Delete Successful" });

        }
    }
}