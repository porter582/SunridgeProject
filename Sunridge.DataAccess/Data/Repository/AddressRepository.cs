using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly ApplicationDbContext _db;

        public AddressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetAddressListOrDropdown()
        {
            return _db.Address.Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.Id.ToString()
            });
        }

        public void Update(Address address)
        {
            var objFromDb = _db.Address.FirstOrDefault(s => s.Id == address.Id);
            objFromDb.StreetAddress = address.StreetAddress;
            objFromDb.Apartment = address.Apartment;
            objFromDb.City = address.City;
            objFromDb.State = address.State;
            objFromDb.Zip = address.Zip;
            objFromDb.IsArchive = address.IsArchive;
            objFromDb.LastModifiedBy = address.LastModifiedBy;
            objFromDb.LastModifiedDate = address.LastModifiedDate;


            _db.SaveChanges();
        }
    }
}
