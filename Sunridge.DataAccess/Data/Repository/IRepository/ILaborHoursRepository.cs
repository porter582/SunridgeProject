using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ILaborHoursRepository : IRepository<LaborHours>
    {
        IEnumerable<SelectListItem> GetLaborHoursItemListOrDropdown();

        void Update(LaborHours laborHours);
    }
}
