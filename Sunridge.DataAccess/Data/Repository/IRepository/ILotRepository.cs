using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ILotRepository : IRepository<Lot>
    {
        IEnumerable<SelectListItem> GetLotListOrDropdown();

        void Update(Lot lot);
    }
}
