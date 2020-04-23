using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class InKindWorkHoursRepository : Repository<InKindWorkHours>, IInKindWorkHoursRepository
    {
        private readonly ApplicationDbContext _db;

        public InKindWorkHoursRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetInKindWorkHoursListOrDropdown()
        {
            return _db.InKindWorkHours.Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.Type.ToString()
            });
        }

        public void Update(InKindWorkHours inKindWorkHours)
        {
            var objFromDb = _db.InKindWorkHours.FirstOrDefault(s => s.Id == inKindWorkHours.Id);
            objFromDb.Description = inKindWorkHours.Description;
            objFromDb.Hours = inKindWorkHours.Hours;
            objFromDb.Type = inKindWorkHours.Type;
            objFromDb.FormResponseId = inKindWorkHours.FormResponseId;


            _db.SaveChanges();
        }
    }
}
