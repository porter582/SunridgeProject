using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class DashboardViewModelRepository : Repository<DashboardViewModel>, IDashboardViewModelRepository
    {
        private readonly ApplicationDbContext _db;

        public DashboardViewModelRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDashboardViewModelListOrDropdown()
        {
            return _db.DashboardViewModel.Select(i => new SelectListItem()
            {
                Value = i.Owner.ToString(),
                Text = i.Owner.ToString()
            });
        }

        public void Update(DashboardViewModel dashboardViewModel)
        {
            var objFromDb = _db.DashboardViewModel.FirstOrDefault(s => s.Owner == dashboardViewModel.Owner);
            objFromDb.Owner = dashboardViewModel.Owner;
            objFromDb.Lots = dashboardViewModel.Lots;
            objFromDb.KeyHistories = dashboardViewModel.KeyHistories;



            _db.SaveChanges();
        }
    }
}
