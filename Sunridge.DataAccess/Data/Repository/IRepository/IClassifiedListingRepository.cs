using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClassifiedListingRepository : IRepository<ClassifiedListing>
    {
        IEnumerable<SelectListItem> GetClassifiedListingListOrDropdown();

        void Update(ClassifiedListing applicationUser);
    }
}
