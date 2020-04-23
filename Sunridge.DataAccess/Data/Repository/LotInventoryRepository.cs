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
    public class LotInventoryRepository : Repository<LotInventory>, ILotInventoryRepository
    {
        private readonly ApplicationDbContext _db;

        public LotInventoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetLotInventoryListOrDropdown()
        {
            return _db.LotInventory.Select(i => new SelectListItem()
            {
                Text = i.LotInventoryId.ToString(),
                Value = i.Lot.ToString()
            });
        }

        public void Update(LotInventory lotInventory)
        {
            var objFromDb = _db.LotInventory.FirstOrDefault(s => s.LotInventoryId == lotInventory.LotInventoryId);

            objFromDb.IsArchive = lotInventory.IsArchive;
            objFromDb.Description = lotInventory.Description;
            objFromDb.Inventory = lotInventory.Inventory;
            objFromDb.InventoryId = lotInventory.InventoryId;
            objFromDb.LastModifiedBy = lotInventory.LastModifiedBy;
            objFromDb.LastModifiedDate = lotInventory.LastModifiedDate;
            objFromDb.Lot = lotInventory.Lot;
            objFromDb.LotId = lotInventory.LotId;

            _db.SaveChanges();
        }
    }
}
