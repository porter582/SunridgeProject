using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClassifiedImageRepository : IRepository<ClassifiedImage>
    {
        IEnumerable<SelectListItem> GetClassifiedImageListOrDropdown();

        void Update(ClassifiedImage applicationUser);
    }
}
