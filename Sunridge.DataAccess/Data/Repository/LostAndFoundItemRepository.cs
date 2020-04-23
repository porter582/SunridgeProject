using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class LostAndFoundItemRepository : Repository<LostAndFoundItem>, ILostAndFoundItemRepository
    {
        private readonly ApplicationDbContext _db;

        public LostAndFoundItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetLostAndFoundItemListOrDropdown()
        {
            return _db.LostAndFoundItem.Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.Id.ToString()
            });
        }

        public void Update(LostAndFoundItem lostAndFoundItem)
        {
            var objFromDb = _db.LostAndFoundItem.FirstOrDefault(s => s.Id == lostAndFoundItem.Id);
            objFromDb.Description = lostAndFoundItem.Description;
            objFromDb.Image = lostAndFoundItem.Image;
            objFromDb.ApplicationUserId = lostAndFoundItem.ApplicationUserId;
            objFromDb.ApplicationUser = lostAndFoundItem.ApplicationUser;
            objFromDb.username = lostAndFoundItem.ApplicationUser.FullName;

            _db.SaveChanges();
        }
    }
}
