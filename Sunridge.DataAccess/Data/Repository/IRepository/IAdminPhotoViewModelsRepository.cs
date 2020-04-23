using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IAdminPhotoViewModelsRepository : IRepository<AdminPhotoViewModels>
    {
        IEnumerable<SelectListItem> GetAdminPhotoViewModelsListOrDropdown();

        void Update(AdminPhotoViewModels address);
    }
}
