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
    public class ScheduledEventsRepository : Repository<ScheduledEvent>, IScheduledEventsRepository
    {
        private readonly ApplicationDbContext _db;

        public ScheduledEventsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetScheduledEventsListOrDropdown()
        {
            return _db.ScheduledEvent.Select(i => new SelectListItem()
            {
                Text = i.ID.ToString(),
                Value = i.Description.ToString()
            });
        }

        public void Update(ScheduledEvent scheduledEvent)
        {
            var objFromDb = _db.ScheduledEvent.FirstOrDefault(s => s.ID == scheduledEvent.ID);

            objFromDb.Description = scheduledEvent.Description;
            objFromDb.Image = scheduledEvent.Image;
            objFromDb.End = scheduledEvent.End;
            objFromDb.IsFullDay = scheduledEvent.IsFullDay;
            objFromDb.Location = scheduledEvent.Location;
            objFromDb.Start = scheduledEvent.Start;
            objFromDb.Subject = scheduledEvent.Subject;

            _db.SaveChanges();
        }
    }
}
