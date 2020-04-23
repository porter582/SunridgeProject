using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IDashboardViewModelRepository : IRepository<DashboardViewModel>
    {
        IEnumerable<SelectListItem> GetDashboardViewModelListOrDropdown();

        void Update(DashboardViewModel address);
    }
}
