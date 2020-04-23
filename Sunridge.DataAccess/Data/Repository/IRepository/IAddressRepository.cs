using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IAddressRepository : IRepository<Address>
    {
        IEnumerable<SelectListItem> GetAddressListOrDropdown();

        void Update(Address address);
    }
}
