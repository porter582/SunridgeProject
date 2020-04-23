using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ErrorViewModelRepository : Repository<ErrorViewModel>, IErrorViewModelRepository
    {
        private readonly ApplicationDbContext _db;

        public ErrorViewModelRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetErrorViewModelListOrDropdown()
        {
            return _db.ErrorViewModel.Select(i => new SelectListItem()
            {
                Value = i.RequestId.ToString(),
                Text = i.RequestId.ToString()
            });
        }

        public void Update(ErrorViewModel errorViewModel)
        {
            var objFromDb = _db.ErrorViewModel.FirstOrDefault(s => s.RequestId == errorViewModel.RequestId);
            objFromDb.RequestId = errorViewModel.RequestId;


            _db.SaveChanges();
        }
    }
}
