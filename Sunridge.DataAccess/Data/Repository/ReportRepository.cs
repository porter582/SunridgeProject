using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly ApplicationDbContext _db;

        public ReportRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetReportListOrDropdown()
        {
            return _db.ReportItem.Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.Id.ToString()
            });
        }

        public void Update(Report report)
        {
            var objFromDb = _db.ReportItem.FirstOrDefault(s => s.Id == report.Id);
            objFromDb.ApplicationUserId = report.ApplicationUserId;
            objFromDb.FormType = report.FormType;
            objFromDb.ApplicationUser = report.ApplicationUser;
            objFromDb.ListingDate = report.ListingDate;
            objFromDb.ResolvedDate = report.ResolvedDate;
            objFromDb.Description = report.Description;
            objFromDb.Suggestion = report.Suggestion;
            objFromDb.Comments = report.Comments;
            objFromDb.Resolved = report.Resolved;
            objFromDb.username = report.ApplicationUser.FullName;


            _db.SaveChanges();
        }
    }
}
