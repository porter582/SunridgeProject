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
    public class ClassifiedImageRepository : Repository<ClassifiedImage>, IClassifiedImageRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassifiedImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetClassifiedImageListOrDropdown()
        {
            return _db.ClassifiedImage.Select(i => new SelectListItem()
            {
                Value = i.ClassifiedImageId.ToString(),
                Text = i.ImageURL
            });
        }

        public void Update(ClassifiedImage classifiedImage)
        {
            var objFromDb = _db.ClassifiedImage.FirstOrDefault(s => s.ClassifiedImageId == classifiedImage.ClassifiedImageId);
            objFromDb.IsMainImage = classifiedImage.IsMainImage;
            objFromDb.IsMainImage = classifiedImage.IsMainImage;
            objFromDb.ImageExtension = classifiedImage.ImageExtension;



            _db.SaveChanges();
        }
    }
}
