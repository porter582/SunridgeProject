using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class PropDamageClaimReportRepository : Repository<PropDamageClaimReport>, IPropDamageClaimReportRepository
    {
        private readonly ApplicationDbContext _db;

        public PropDamageClaimReportRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetPropDamageClaimReportFromDropDown()
        {
            return _db.PropDamageClaimReport.Select(i => new SelectListItem()
            {
                Text = i.id.ToString(),
                Value = i.username.ToString()
            });
        }

        public void Update(PropDamageClaimReport propDamageClaimReport)
        {
            var objFromDb = _db.PropDamageClaimReport.FirstOrDefault(s => s.id == propDamageClaimReport.id);

            objFromDb.username = propDamageClaimReport.username;
            objFromDb.ApplicationUser = propDamageClaimReport.ApplicationUser;
            objFromDb.ApplicationUserId = propDamageClaimReport.ApplicationUserId;
            objFromDb.FileName = propDamageClaimReport.FileName;
            objFromDb.FilePath = propDamageClaimReport.FilePath;
            objFromDb.listingDate = propDamageClaimReport.listingDate;
            objFromDb.resolved = propDamageClaimReport.resolved;
            objFromDb.comments = propDamageClaimReport.comments;
            objFromDb.resolveddate = propDamageClaimReport.resolveddate;

            _db.SaveChanges();
        }
    }
}
