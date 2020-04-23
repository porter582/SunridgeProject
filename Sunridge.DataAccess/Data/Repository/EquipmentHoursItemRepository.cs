using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class EquipmentHoursItemRepository : Repository<EquipmentHours>, IEquipmentHoursRepository
    {
        private readonly ApplicationDbContext _db;

        public EquipmentHoursItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetEquipmentHoursItemListOrDropdown()
        {
            return _db.EquipmentHoursItem.Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.Id.ToString()
            });
        }

        public void Update(EquipmentHours equipmentHours)
        {
            var objFromDb = _db.EquipmentHoursItem.FirstOrDefault(s => s.Id == equipmentHours.Id);
            objFromDb.EquipmentName = equipmentHours.EquipmentName;
            objFromDb.Hours = equipmentHours.Hours;
            objFromDb.Report = equipmentHours.Report;
            objFromDb.ReportId = equipmentHours.ReportId;
            _db.SaveChanges();
        }
    }
}
