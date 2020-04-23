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
    public class PhotoCategoriesRepository : Repository<PhotoCategories>, IPhotoCategoriesRepository
    {
        private readonly ApplicationDbContext _db;

        public PhotoCategoriesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetPhotoCategoriesListOrDropdown()
        {
            return _db.PhotoCategories.Select(i => new SelectListItem()
            {
                Text = i.CategoryName.ToString(),
                Value = i.CategoryName.ToString()
            });
        }

        public void Update(PhotoCategories photoCategories)
        {
            var objFromDb = _db.PhotoCategories.FirstOrDefault(s => s.PhotoCategoriesId == photoCategories.PhotoCategoriesId);

            objFromDb.CategoryName = photoCategories.CategoryName;

            _db.SaveChanges();
        }
    }
}
