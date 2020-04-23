using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ITransactionRepository : IRepository<Sunridge.Models.Transaction>
    {
        IEnumerable<SelectListItem> GetTransactionListOrDropdown();

        void Update(Sunridge.Models.Transaction transaction);
    }
}
