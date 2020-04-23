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
    public class KeyHistoryRepository : Repository<KeyHistory>, IKeyHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public KeyHistoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetKeyHistoryListOrDropdown()
        {
            return _db.KeyHistory.Select(i => new SelectListItem()
            {
                Text = i.KeyHistoryId.ToString(),
                Value = i.KeyHistoryId.ToString()
            });
        }

        public void Update(KeyHistory keyHistory)
        {
              var objFromDb = _db.KeyHistory.FirstOrDefault(s => s.KeyHistoryId == keyHistory.KeyHistoryId);

            objFromDb.IsArchive = keyHistory.IsArchive;
            objFromDb.DateIssued = keyHistory.DateIssued;
            objFromDb.DateReturned = keyHistory.DateReturned;
            objFromDb.Key = keyHistory.Key;
            objFromDb.KeyId = keyHistory.KeyId;
            objFromDb.LastModifiedBy = keyHistory.LastModifiedBy;
            objFromDb.LastModifiedDate = keyHistory.LastModifiedDate;
            objFromDb.Lot = keyHistory.Lot;
            objFromDb.LotId = keyHistory.LotId;
            objFromDb.PaidAmount = keyHistory.PaidAmount;
            objFromDb.Status = keyHistory.Status;

            _db.SaveChanges();
        }
    }
}
