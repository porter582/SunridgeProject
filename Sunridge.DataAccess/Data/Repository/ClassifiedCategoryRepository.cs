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
    public class ClassifiedCategoryRepository : Repository<ClassifiedCategory>, IClassifiedCategoryRepository
    {
            private readonly ApplicationDbContext _db;

            public ClassifiedCategoryRepository(ApplicationDbContext db) : base(db)
            {
                _db = db;
            }

            public IEnumerable<SelectListItem> GetClassifiedCategoryListOrDropdown()
            {
                return _db.ClassifiedCategory.Select(i => new SelectListItem()
                {
                    Value = i.ClassifiedCategoryId.ToString(),
                    Text = i.Description.ToString()
                });
            }

            public void Update(ClassifiedCategory classifiedCategory)
            {
                var objFromDb = _db.ClassifiedCategory.FirstOrDefault(s => s.ClassifiedCategoryId == classifiedCategory.ClassifiedCategoryId);
                objFromDb.Description = classifiedCategory.Description;

                _db.SaveChanges();
            }
        }
    }

