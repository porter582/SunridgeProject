using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClassifiedListingVMRepository : IRepository<ClassifiedListingVM>
    {
        IEnumerable<SelectListItem> GetClassifiedListingVMListOrDropdown();

        void Update(ClassifiedListingVM address);
    }
}
