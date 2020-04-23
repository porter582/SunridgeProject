using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ILotInventoryRepository : IRepository<LotInventory>
    {
        IEnumerable<SelectListItem> GetLotInventoryListOrDropdown();

        void Update(LotInventory lotInventory);
    }
}
