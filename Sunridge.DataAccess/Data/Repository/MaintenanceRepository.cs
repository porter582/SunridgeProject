using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
    {
        private readonly ApplicationDbContext _db;

        public MaintenanceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetMaintenanceListOrDropdown()
        {
            return _db.Maintenance.Select(i => new SelectListItem()
            {
                Text = i.MaintenanceId.ToString(),
                Value = i.Description.ToString()
            });
        }

        public void Update(Maintenance maintenance)
        {
            var objFromDb = _db.Maintenance.FirstOrDefault(s => s.MaintenanceId == maintenance.MaintenanceId);

            objFromDb.IsArchive = maintenance.IsArchive;
            objFromDb.Description = maintenance.Description;
            objFromDb.CommonAreaAsset = maintenance.CommonAreaAsset;
            objFromDb.CommonAreaAssetId = maintenance.CommonAreaAssetId;
            objFromDb.LastModifiedBy = maintenance.LastModifiedBy;
            objFromDb.LastModifiedDate = maintenance.LastModifiedDate;
            objFromDb.Cost = maintenance.Cost;
            objFromDb.DateCompleted = maintenance.DateCompleted;
            
            _db.SaveChanges();
        }
    }
}
