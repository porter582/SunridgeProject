using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class LaborHoursItemRepository : Repository<LaborHours>, ILaborHoursRepository
    {
        private readonly ApplicationDbContext _db;

        public LaborHoursItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetLaborHoursItemListOrDropdown()
        {
            return _db.LaborHoursItem.Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.Id.ToString()
            });
        }

        public void Update(LaborHours laborHours)
        {
            var objFromDb = _db.LaborHoursItem.FirstOrDefault(s => s.Id == laborHours.Id);
            objFromDb.LaborActivity = laborHours.LaborActivity;
            objFromDb.Hours = laborHours.Hours;
            objFromDb.Report = laborHours.Report;
            objFromDb.ReportId = laborHours.ReportId;


            _db.SaveChanges();
        }
    }
}
