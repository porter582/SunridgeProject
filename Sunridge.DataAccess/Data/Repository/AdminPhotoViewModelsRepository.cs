using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class AdminPhotoViewModelsRepository : Repository<AdminPhotoViewModels>, IAdminPhotoViewModelsRepository
    {
        private readonly ApplicationDbContext _db;

        public AdminPhotoViewModelsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetAdminPhotoViewModelsListOrDropdown()
        {
            return _db.AdminPhotoViewModels.Select(i => new SelectListItem()
            {
                Value = i.Photo.ToString(),
                Text = i.Categories.ToString()
            });
        }

        public void Update(AdminPhotoViewModels address)
        {
            var objFromDb = _db.AdminPhotoViewModels.FirstOrDefault(s => s.Photo == address.Photo);
            objFromDb.Categories = address.Categories;



            _db.SaveChanges();
        }
    }
}
