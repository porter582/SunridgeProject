using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        private readonly ApplicationDbContext _db;

        public InventoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetInventoryListOrDropdown()
        {
            return _db.Inventory.Select(i => new SelectListItem()
            {
                Value = i.InventoryId.ToString(),
                Text = i.Description.ToString()
            });
        }

        public void Update(Inventory inventory)
        {
            var objFromDb = _db.Inventory.FirstOrDefault(s => s.InventoryId == inventory.InventoryId);
            objFromDb.Description = inventory.Description;
            objFromDb.IsArchive = inventory.IsArchive;
            objFromDb.LastModifiedBy = inventory.LastModifiedBy;
            objFromDb.LastModifiedDate = inventory.LastModifiedDate;
            objFromDb.LotInventories = inventory.LotInventories;


            _db.SaveChanges();
        }
    }
}
