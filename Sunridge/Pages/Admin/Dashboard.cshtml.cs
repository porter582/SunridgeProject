using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<Report> ReportsList { get; set; }
        public IEnumerable<PropDamageClaimReport> ClaimReportList { get; set; }

        public DashboardModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            ReportsList = _unitOfWork.ReportItem.GetAll(u=> u.Resolved == false);
            ClaimReportList = _unitOfWork.PropDamageClaimReport.GetAll(u=> u.resolved == false);
        }
    }
}