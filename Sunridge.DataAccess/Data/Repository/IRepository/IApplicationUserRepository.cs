using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<SelectListItem> GetApplicationUserListOrDropdown();
        IEnumerable<SelectListItem> GetApplicationUserListOrDropdown(string id);

        public int AddAddressAndGetId(ApplicationUser applicationUser);

        void Update(ApplicationUser applicationUser);
    }
}
