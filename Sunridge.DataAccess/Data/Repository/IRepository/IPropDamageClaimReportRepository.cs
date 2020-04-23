using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IPropDamageClaimReportRepository : IRepository<PropDamageClaimReport>
    {
        IEnumerable<SelectListItem> GetPropDamageClaimReportFromDropDown();

        void Update(PropDamageClaimReport propDamageClaimReport);
    }
}
