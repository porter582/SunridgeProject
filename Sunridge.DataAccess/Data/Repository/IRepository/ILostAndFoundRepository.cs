using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ILostAndFoundItemRepository : IRepository<LostAndFoundItem>
    {
        IEnumerable<SelectListItem> GetLostAndFoundItemListOrDropdown();

        void Update(LostAndFoundItem address);
    }
}
