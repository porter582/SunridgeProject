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
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        private readonly ApplicationDbContext _db;

        public PhotoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetPhotoListOrDropdown()
        {
            return _db.Photo.Select(i => new SelectListItem()
            {
                Text = i.Id.ToString(),
                Value = i.Title.ToString()
            });
        }

        public void Update(Photo photo)
        {
            var objFromDb = _db.Photo.FirstOrDefault(s => s.Id == photo.Id);

            objFromDb.Category = photo.Category;
            objFromDb.Image = photo.Image;
            objFromDb.Name = photo.Name;
            objFromDb.Title = photo.Title;
            objFromDb.Year = photo.Year;

            _db.SaveChanges();
        }
    }
}
