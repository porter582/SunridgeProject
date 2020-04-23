using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class AdminCommentsRepository : Repository<AdminComments>, IAdminCommentsRepository
    {
        private readonly ApplicationDbContext _db;

        public AdminCommentsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetLaborHoursItemListOrDropDown()
        {
            return _db.LaborHoursItem.Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.Id.ToString()
            });
        }

        public void Update(AdminComments adminComments)
        {
            var objFromDb = _db.AdminComments.FirstOrDefault(s => s.Id == adminComments.Id);
            objFromDb.AdminComment = adminComments.AdminComment;
            objFromDb.ReportId = adminComments.ReportId;
            objFromDb.Report = adminComments.Report;
            objFromDb.ApplicationUserId = adminComments.ApplicationUserId;
            objFromDb.ApplicationUser = adminComments.ApplicationUser;


            _db.SaveChanges();
        }
    }
}
