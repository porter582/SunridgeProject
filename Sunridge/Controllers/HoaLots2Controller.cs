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
    public class HoaLots2Controller : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public List<HOALotVM> HoaLots { get; set; }

       public HoaLots2Controller(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }


        //This is identical to the HoaLotsController.cs Delete method with the exception that it filters by 
        //not primary owner instead of by primary owner. There wasn't an easy way to handle which user was
        //being archived via datatables calling this delete method with just the one controller.
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
                    if (!ownerLot.IsPrimary)
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
                return Json(new { success = false, message = "Error while deleting" });
            }

            return Json(new { success = true, message = "Delete successful." });
        }
    }
}