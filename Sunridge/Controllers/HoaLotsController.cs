using System;
using System.IO;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunridge.Utility;
using System.Security.Claims;
using Sunridge.Models.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaLotsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public List<HOALotVM> HoaLots { get; set; }

        public HoaLotsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            HoaLots = new List<HOALotVM>();
            List<Models.Lot> theLots = new List<Models.Lot>();

            if (User.IsInRole(SD.AdminRole))
            {
                var eachLot = _unitOfWork.Lot.GetAll(null, null, null);

                foreach (var lot in eachLot)
                {
                    theLots.Add(lot);
                }
            }
            else
            {
                var ownerLots = _unitOfWork.OwnerLot.GetAll(s => s.OwnerId == ClaimTypes.NameIdentifier);

                foreach (var lot in ownerLots)
                {
                    var eachLot = _unitOfWork.Lot.GetAll(s => s.LotId == lot.LotId, null, null);

                    foreach (var l in eachLot)
                    {
                        theLots.Add(l);
                    }
                }

            }
            HOALotVM tempModel;

            foreach(var lot in theLots)
            {
                tempModel = new HOALotVM();

                // set lotnumber and taxid and id
                tempModel.id = lot.LotId;
                tempModel.LotNumber = lot.LotNumber;
                tempModel.TaxId = lot.TaxId;

                //set address value
                var address = _unitOfWork.Address.GetFirstOrDefault(s => s.Id == lot.AddressId);
                string addr = $"{address.StreetAddress} {address.Apartment} {address.City}, {address.State} {address.Zip}";
                tempModel.StreetAddress = addr;

                //get owner(s) for lot
                string theOwners = "";
                var ownerLots = _unitOfWork.OwnerLot.GetAll(s => s.LotId == lot.LotId);
                //add owner(s) to string
                foreach(var oLot in ownerLots)
                {
                    var owner = _unitOfWork.ApplicationUser.GetFirstOrDefault(s => s.Id == oLot.OwnerId);

                    if (oLot.IsPrimary && owner != null)
                    {
                        theOwners += $"<strong>{owner.FullName}</strong>, ";
                        tempModel.primaryOwnerId = oLot.OwnerId;
                    }
                }

                foreach (var oLot in ownerLots)
                {
                    var owner = _unitOfWork.ApplicationUser.GetFirstOrDefault(s => s.Id == oLot.OwnerId);

                    if (!oLot.IsPrimary && owner != null)
                    {
                        theOwners += owner.FullName + ", ";
                        tempModel.secondaryOwnerId = oLot.OwnerId;
                    }
                }

                //trim last comma
                if (theOwners.Length > 1)
                    theOwners = theOwners.Substring(0, theOwners.Length - 2);

                HttpUtility.HtmlEncode(theOwners);

                //add owner(s) to model
                tempModel.UserName = theOwners;

                //get and set lot inventory data
                var lotInventories = _unitOfWork.LotInventory.GetAll(s => s.LotId == lot.LotId, null, null);
                string theInventories = "";
                foreach (var lotInventory in lotInventories)
                {
                    var inventoryItem = _unitOfWork.Inventory.GetFirstOrDefault(s => s.InventoryId == lotInventory.InventoryId);
                    theInventories += inventoryItem.Description + ", ";
                }

                if (theInventories.Length > 1)
                    theInventories = theInventories.Substring(0, theInventories.Length - 2);

                tempModel.LotInventory = theInventories;

                HoaLots.Add(tempModel);
            }

            return Json(new { data = HoaLots });
        }

        [HttpDelete("{lotNum}")]
        public IActionResult Delete(string lotNum)
        {
            try
            {
                var lot = _unitOfWork.Lot.GetFirstOrDefault(s => s.LotNumber == lotNum);
                //remove owner from lot and then archive owner
                var ownerLots = _unitOfWork.OwnerLot.GetAll(s => s.LotId == lot.LotId);

                foreach (var ownerLot in ownerLots)
                {
                    if (ownerLot.IsPrimary)
                    {
                        var owner = _unitOfWork.ApplicationUser.GetFirstOrDefault(s => s.Id == ownerLot.OwnerId);

                        ownerLot.OwnerId = null;

                        _unitOfWork.OwnerLot.Update(ownerLot);
                        _unitOfWork.Save();

                        owner.IsArchive = true;
                        _unitOfWork.ApplicationUser.Update(owner);
                        _unitOfWork.Save();
                    }
                } 
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            return Json(new { success = true, message = "Delete successful." });
        }
    }
}