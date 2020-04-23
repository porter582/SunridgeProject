using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IErrorViewModelRepository : IRepository<ErrorViewModel>
    {
        IEnumerable<SelectListItem> GetErrorViewModelListOrDropdown();

        void Update(ErrorViewModel address);
    }
}
