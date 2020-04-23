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
    public class OwnerLotRepository : Repository<OwnerLot>, IOwnerLotRepository
    {
        private readonly ApplicationDbContext _db;

        public OwnerLotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetOwnerLotListOrDropdown()
        {
            return _db.OwnerLot.Select(i => new SelectListItem()
            {
                Text = i.OwnerLotId.ToString(),
                Value = i.Lot.ToString()
            });
        }

        public void Update(OwnerLot ownerLot)
        {
            var objFromDb = _db.OwnerLot.FirstOrDefault(s => s.OwnerLotId == ownerLot.OwnerLotId);

            objFromDb.EndDate = ownerLot.EndDate;
            objFromDb.IsArchive = ownerLot.IsArchive;
            objFromDb.IsPrimary = ownerLot.IsPrimary;
            objFromDb.LastModifiedBy = ownerLot.LastModifiedBy;
            objFromDb.LastModifiedDate = ownerLot.LastModifiedDate;
            objFromDb.LotId = ownerLot.LotId;
            objFromDb.OwnerId = ownerLot.OwnerId;
            objFromDb.StartDate = ownerLot.StartDate;

            _db.SaveChanges();
        }
    }
}
