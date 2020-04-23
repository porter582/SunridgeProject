using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<SelectListItem> GetCommentListOrDropdown();

        void Update(Comment address);
    }
}
