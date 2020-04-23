using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IPhotoCategoriesRepository : IRepository<PhotoCategories>
    {
        IEnumerable<SelectListItem> GetPhotoCategoriesListOrDropdown();

        void Update(PhotoCategories photoCategories);
    }
}
