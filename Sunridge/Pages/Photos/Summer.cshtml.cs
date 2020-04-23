using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Photos
{
    public class SummerModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<Photo> photos { get; set; }

        public SummerModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            photos = _unitOfWork.Photo.GetAll(null, null, null);
        }
    }
}