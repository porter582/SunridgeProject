using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Admin.Owners
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        //public IEnumerable<ApplicationUser> owners { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
        //    owners = _unitOfWork.ApplicationUser.GetAll(s => s.IsArchive == false, null, null);

        //    foreach (var owner in owners)
        //    {
        //        owner.OwnerLots = _unitOfWork.OwnerLot.GetAll(s => s.OwnerId == owner.Id, null, null);
        //        foreach (var lot in owner.OwnerLots)
        //        {
        //            lot.Lot = _unitOfWork.Lot.GetFirstOrDefault(s => s.LotId == lot.LotId);
        //        }
        //    }
        }
    }
}