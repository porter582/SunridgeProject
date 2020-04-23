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
    public class LotRepository : Repository<Lot>, ILotRepository
    {
        private readonly ApplicationDbContext _db;

        public LotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetLotListOrDropdown()
        {
            return _db.Lot.Select(i => new SelectListItem()
            {
                Text = i.LotNumber.ToString(),
                Value = i.LotId.ToString()
            });
        }

        public void Update(Lot lot)
        {
            var objFromDb = _db.Lot.FirstOrDefault(s => s.LotId == lot.LotId);

            objFromDb.Address = lot.Address;
            objFromDb.AddressId = lot.AddressId;
            objFromDb.IsArchive = lot.IsArchive;
            objFromDb.LastModifiedBy = lot.LastModifiedBy;
            objFromDb.LastModifiedDate = lot.LastModifiedDate;
            objFromDb.LotHistories = lot.LotHistories;
            objFromDb.LotInventories = lot.LotInventories;
            objFromDb.LotNumber = lot.LotNumber;
            objFromDb.OwnerLots = lot.OwnerLots;
            objFromDb.TaxId = lot.TaxId;
            objFromDb.Transactions = lot.Transactions;

            _db.SaveChanges();
        }
    }
}
