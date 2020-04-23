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
    public class LotHistoryRepository : Repository<LotHistory>, ILotHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public LotHistoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetLotHistoryListOrDropdown()
        {
            return _db.LotHistory.Select(i => new SelectListItem()
            {
                Text = i.LotHistoryId.ToString(),
                Value = i.Lot.ToString()
            });
        }

        public void Update(LotHistory LotHistory)
        {
            var objFromDb = _db.LotHistory.FirstOrDefault(s => s.LotHistoryId == LotHistory.LotHistoryId);

            objFromDb.IsArchive = LotHistory.IsArchive;
            objFromDb.Comments = LotHistory.Comments;
            //objFromDb.Files = LotHistory.Files;
            objFromDb.LastModifiedBy = LotHistory.LastModifiedBy;
            objFromDb.LastModifiedDate = LotHistory.LastModifiedDate;
            objFromDb.Lot = LotHistory.Lot;
            objFromDb.LotHistoryId = LotHistory.LotHistoryId;
            objFromDb.LotId = LotHistory.LotId;
            objFromDb.PrivacyLevel = LotHistory.PrivacyLevel;

            _db.SaveChanges();
        }
    }
}
