using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IFileRepository : IRepository<File>
    {
        IEnumerable<SelectListItem> GetFileListOrDropdown();

        void Update(File address);
    }
}
