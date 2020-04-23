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
    public class ClassifiedListingRepository : Repository<ClassifiedListing>, IClassifiedListingRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassifiedListingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetClassifiedListingListOrDropdown()
        {
            return _db.ClassifiedListing.Select(i => new SelectListItem()
            {
                Value = i.ClassifiedListingId.ToString(),
                Text = i.ItemName
            });
        }

        public void Update(ClassifiedListing classifiedListing)
        {
            var objFromDb = _db.ClassifiedListing.FirstOrDefault(s => s.ClassifiedListingId == classifiedListing.ClassifiedListingId);
            objFromDb.Owner = classifiedListing.Owner;
            objFromDb.ItemName = classifiedListing.ItemName;
            objFromDb.Price = classifiedListing.Price;
            objFromDb.Description = classifiedListing.Description;
            objFromDb.ListingDate = classifiedListing.ListingDate;
            objFromDb.Phone = classifiedListing.Phone;
            objFromDb.Email = classifiedListing.Email;
            objFromDb.Category = classifiedListing.Category;
            objFromDb.Images = classifiedListing.Images;
            objFromDb.Categories = classifiedListing.Categories;
            objFromDb.classifiedcategory = classifiedListing.classifiedcategory;

            _db.SaveChanges();
        }
    }
}
