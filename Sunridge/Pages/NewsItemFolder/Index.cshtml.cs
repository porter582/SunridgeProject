using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.NewsItemFolder
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<Models.NewsItem> NewsItemList { get; set; }
        public List<int> numYears { get; set; }
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            NewsItemList = _unitOfWork.NewsItem.GetAll(null, q => q.OrderByDescending(c => c.Year), null);
            numYears = new List<int>();
            foreach(var item in NewsItemList)
            {
                if(numYears.Contains(item.Year))
                {
                    continue;
                }
                else
                {
                    numYears.Add(item.Year);
                }
            }

        }
    }
}