using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.LostAndFound
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<LostAndFoundItem> LostAndFoundItemList { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUserList { get; set; }
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            LostAndFoundItemList = _unitOfWork.LostAndFoundItem.GetAll(null, null, null);
            ApplicationUserList = _unitOfWork.ApplicationUser.GetAll(null, null, null);

        }
    }
}