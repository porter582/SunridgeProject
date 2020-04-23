using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class FormResponseRepository : Repository<FormResponse>, IFormResponseRepository
    {
        private readonly ApplicationDbContext _db;

        public FormResponseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetFormResponseListOrDropdown()
        {
            return _db.FormResponse.Select(i => new SelectListItem()
            {
                Value = i.FormResponseId.ToString(),
                Text = i.FormType.ToString()
            });
        }

        public void Update(FormResponse formResponse)
        {
            var objFromDb = _db.FormResponse.FirstOrDefault(s => s.FormResponseId == formResponse.FormResponseId);
            objFromDb.OwnerId = formResponse.OwnerId;
            objFromDb.FormType = formResponse.FormType;
            objFromDb.LotId = formResponse.LotId;
            objFromDb.SubmitDate = formResponse.SubmitDate;
            objFromDb.Description = formResponse.Description;
            objFromDb.Suggestion = formResponse.Suggestion;
            objFromDb.PrivacyLevel = formResponse.PrivacyLevel;
            objFromDb.Resolved = formResponse.Resolved;
            objFromDb.ResolveUser = formResponse.ResolveUser;
            objFromDb.Owner = formResponse.Owner;
            objFromDb.Comments = formResponse.Comments;
            //objFromDb.Files = formResponse.Files;
            objFromDb.Lot = formResponse.Lot;
            objFromDb.InKindWorkHours = formResponse.InKindWorkHours;


            _db.SaveChanges();
        }
    }
}
