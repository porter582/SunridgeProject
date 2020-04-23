using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ICommonAreaAssetRepository : IRepository<CommonAreaAsset>
    {
        IEnumerable<SelectListItem> GetCommonAreaAssetListOrDropdown();

        void Update(CommonAreaAsset address);
    }
}
