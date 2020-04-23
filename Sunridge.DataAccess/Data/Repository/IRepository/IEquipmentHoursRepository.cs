using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IEquipmentHoursRepository : IRepository<EquipmentHours>
    {
        IEnumerable<SelectListItem> GetEquipmentHoursItemListOrDropdown();

        void Update(EquipmentHours equipmentHours);
    }
}
