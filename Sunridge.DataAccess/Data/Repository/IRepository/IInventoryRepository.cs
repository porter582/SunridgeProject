using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        IEnumerable<SelectListItem> GetInventoryListOrDropdown();

        void Update(Inventory address);//
    }
}
