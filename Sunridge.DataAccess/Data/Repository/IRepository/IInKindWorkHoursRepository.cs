using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IInKindWorkHoursRepository : IRepository<InKindWorkHours>
    {
        IEnumerable<SelectListItem> GetInKindWorkHoursListOrDropdown();

        void Update(InKindWorkHours address);
    }
}
