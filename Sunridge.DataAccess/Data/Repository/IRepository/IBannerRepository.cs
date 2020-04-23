using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IBannerRepository : IRepository<Banner>
    {
        IEnumerable<SelectListItem> GetBannerListOrDropdown();

        void Update(Banner banner);
    }
}
