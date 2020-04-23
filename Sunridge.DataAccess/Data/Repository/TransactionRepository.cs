using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class TransactionRepository : Repository<Sunridge.Models.Transaction>, ITransactionRepository
    {
        private readonly ApplicationDbContext _db;

        public TransactionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetTransactionListOrDropdown()
        {
            return _db.Transaction.Select(i => new SelectListItem()
            {
                Text = i.TransactionId.ToString(),
                Value = i.Description.ToString()
            });
        }

        public void Update(Sunridge.Models.Transaction transaction)
        {
            var objFromDb = _db.Transaction.FirstOrDefault(s => s.TransactionId == transaction.TransactionId);

            objFromDb.Description = transaction.Description;
            objFromDb.Amount = transaction.Amount;
            objFromDb.DateAdded = transaction.DateAdded;
            objFromDb.DatePaid = transaction.DatePaid;
            objFromDb.IsArchive = transaction.IsArchive;
            objFromDb.LastModifiedBy = transaction.LastModifiedBy;
            objFromDb.LastModifiedDate = transaction.LastModifiedDate;
            objFromDb.Lot = transaction.Lot;
            objFromDb.LotId = transaction.LotId;
            objFromDb.Owner = transaction.Owner;
            objFromDb.OwnerId = transaction.OwnerId;
            objFromDb.Status = transaction.Status;
            objFromDb.TransactionType = transaction.TransactionType;
            objFromDb.TransactionTypeId = transaction.TransactionTypeId;

            _db.SaveChanges();
        }
    }
}
