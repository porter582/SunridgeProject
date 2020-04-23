using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Admin.Keys
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<KeyHistory> KeyList { get; set; }
        public IEnumerable<OwnerLot> userLots { get; set; }
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //keep track of all of the owners lots
            userLots = _unitOfWork.OwnerLot.GetAll(l => l.OwnerId == claim.Value);

            //get all keys - we will filter by owner in cshtml page
            KeyList = _unitOfWork.KeyHistory.GetAll(null,null,"Key,Lot");

        }
    }
}