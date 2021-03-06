﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IReportRepository : IRepository<Report>
    {
        IEnumerable<SelectListItem> GetReportListOrDropdown();

        void Update(Report report);
    }
}
