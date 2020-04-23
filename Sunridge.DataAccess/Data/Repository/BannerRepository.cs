using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        private readonly ApplicationDbContext _db;

        public BannerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetBannerListOrDropdown()
        {
            return _db.Banner.Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.Header
            });
        }

        public void Update(Banner banner)
        {
            var objFromDb = _db.Banner.FirstOrDefault(s => s.Id == banner.Id);
            objFromDb.Header = banner.Header;
            objFromDb.Body = banner.Body;
            objFromDb.Image = banner.Image;
            _db.SaveChanges();
        }
    }
}
