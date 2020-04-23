using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IAdminCommentsRepository : IRepository<AdminComments>
    {
        void Update(AdminComments adminComments);
    }
}
