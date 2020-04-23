using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IFormResponseRepository : IRepository<FormResponse>
    {
        IEnumerable<SelectListItem> GetFormResponseListOrDropdown();

        void Update(FormResponse address);
    }
}
