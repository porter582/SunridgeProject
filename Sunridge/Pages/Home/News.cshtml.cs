using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Home
{
    public class NewsModel : PageModel
    {
        public readonly IUnitOfWork _unitOfWork;

        public NewsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Sunridge.Models.NewsItem> NewsItems { get; set; }

        public void OnGet()
        {

        }
    }
}