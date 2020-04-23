using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class FileRepository : Repository<File>, IFileRepository
    {
        private readonly ApplicationDbContext _db;

        public FileRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetFileListOrDropdown()
        {
            return _db.File.Select(i => new SelectListItem()
            {
                Value = i.FileId.ToString(),
                Text = i.Name
            });
        }

        public void Update(File file)
        {
            var objFromDb = _db.File.FirstOrDefault(s => s.FileId == file.FileId);
            //objFromDb.LotHistoryId = file.LotHistoryId;
            objFromDb.FileURL = file.FileURL;
            objFromDb.Name = file.Name;
            objFromDb.Description = file.Description;
            //objFromDb.LotHistory = file.LotHistory;


            _db.SaveChanges();
        }
    }
}
