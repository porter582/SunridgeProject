using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class CommonAreaAssetRepository : Repository<CommonAreaAsset>, ICommonAreaAssetRepository
    {
        private readonly ApplicationDbContext _db;

        public CommonAreaAssetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCommonAreaAssetListOrDropdown()
        {
            return _db.CommonAreaAsset.Select(i => new SelectListItem()
            {
                Value = i.CommonAreaAssetId.ToString(),
                Text = i.AssetName
            });
        }

        public void Update(CommonAreaAsset commonAreaAsset)
        {
            var objFromDb = _db.CommonAreaAsset.FirstOrDefault(s => s.CommonAreaAssetId == commonAreaAsset.CommonAreaAssetId);
            objFromDb.AssetName = commonAreaAsset.AssetName;
            objFromDb.PurchasePrice = commonAreaAsset.PurchasePrice;
            objFromDb.Description = commonAreaAsset.Description;
            objFromDb.Status = commonAreaAsset.Status;
            objFromDb.Date = commonAreaAsset.Date;
            objFromDb.Maintenances = commonAreaAsset.Maintenances;


            _db.SaveChanges();
        }
    }
}
