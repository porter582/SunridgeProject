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
    public class ClassifiedServiceRepository : Repository<ClassifiedService>, IClassifiedServiceRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassifiedServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetClassifiedServiceListOrDropdown()
        {
            return _db.ClassifiedService.Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.Id.ToString()
            });
        }

        public void Update(ClassifiedService classifiedService)
        {
            var objFromDb = _db.ClassifiedService.FirstOrDefault(s => s.Id == classifiedService.Id);
            objFromDb.Description = classifiedService.Description;
            objFromDb.Image = classifiedService.Image;
            objFromDb.ApplicationUserId = classifiedService.ApplicationUserId;
            objFromDb.ApplicationUser = classifiedService.ApplicationUser;

            _db.SaveChanges();
        }
    }
}
