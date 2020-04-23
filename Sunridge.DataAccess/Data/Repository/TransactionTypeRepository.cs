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
    public class TransactionTypeRepository : Repository<TransactionType>, ITransactionTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public TransactionTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetTransactionTypeListOrDropdown()
        {
            return _db.TransactionType.Select(i => new SelectListItem()
            {
                Text = i.Id.ToString(),
                Value = i.Description.ToString()
            });
        }

        public void Update(TransactionType transactionType)
        {
            var objFromDb = _db.TransactionType.FirstOrDefault(s => s.Id == transactionType.Id);

            objFromDb.Description = transactionType.Description;
            objFromDb.IsArchive = transactionType.IsArchive;
            objFromDb.LastModifiedBy = transactionType.LastModifiedBy;
            objFromDb.LastModifiedDate = transactionType.LastModifiedDate;

            _db.SaveChanges();
        }
    }
}
