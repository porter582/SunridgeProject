using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetApplicationUserListOrDropdown()
        {
            return _db.ApplicationUser.Where(s => s.IsArchive == false).Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.FullName,
            });
        }

        public IEnumerable<SelectListItem> GetApplicationUserListOrDropdown(string id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var users = _db.ApplicationUser.Where(s => s.IsArchive == false);

            foreach (var user in users)
            {
                items.Add(new SelectListItem()
                {
                    Text = user.FullName,
                    Value = user.Id,
                    Selected = user.Id == id ? true : false
                });
            }

            return items;

            //return _db.ApplicationUser.Where(s => s.IsArchive == false).Select(i => new SelectListItem()
            //{
            //    Value = i.Id.ToString(),
            //    Text = i.FullName,
            //    Selected = i.Id == id ? true : false
            //});
        }

        public int AddAddressAndGetId(ApplicationUser applicationUser)
        {
            Address addr = new Address();
            addr.Apartment = applicationUser.ApartmentValue;
            addr.City = applicationUser.CityValue;
            addr.State = applicationUser.StateValue;
            addr.StreetAddress = applicationUser.AddressValue;
            addr.Zip = applicationUser.ZipValue;

            _db.Address.Add(addr);

            _db.SaveChanges();

            int addrId;

            try
            {
                addrId = _db.Address.Select(s => s.Id).Max();
            }
            catch(Exception e)
            {
                var pants = e;
                addrId = 0;
            }

            return addrId;
        }

        public void Update(ApplicationUser applicationUser)
        {
            var objFromDb = _db.ApplicationUser.FirstOrDefault(s => s.Id == applicationUser.Id);
            objFromDb.FirstName = applicationUser.FirstName;
            objFromDb.LastName = applicationUser.LastName;
            objFromDb.OwnerLots = applicationUser.OwnerLots;
            objFromDb.AddressId = applicationUser.AddressId;
            objFromDb.Address = applicationUser.Address;
            objFromDb.Occupation = applicationUser.Occupation;
            objFromDb.Birthday = applicationUser.Birthday;
            objFromDb.PhoneNumber = applicationUser.PhoneNumber;
            objFromDb.Email = applicationUser.Email;
            objFromDb.EmergencyContactName = applicationUser.EmergencyContactName;
            objFromDb.EmergencyContactPhone = applicationUser.EmergencyContactPhone;
            objFromDb.ReceiveEmails = applicationUser.ReceiveEmails;
            objFromDb.IsArchive = applicationUser.IsArchive;
            objFromDb.LastModifiedBy = applicationUser.LastModifiedBy;
            objFromDb.LastModifiedDate = applicationUser.LastModifiedDate;
            objFromDb.Address = applicationUser.Address;
            objFromDb.Transactions = applicationUser.Transactions;
            objFromDb.FormResponses = applicationUser.FormResponses;
            objFromDb.KeyHistories = applicationUser.KeyHistories;
            objFromDb.LostAndFoundItems = applicationUser.LostAndFoundItems;
            objFromDb.ClassifiedListings = applicationUser.ClassifiedListings;

            Address addr;
            
            addr = _db.Address.Where(s => s.Id == objFromDb.AddressId).FirstOrDefault();

            if (addr == null)
            {
                addr = new Address();
                addr.IsArchive = false;
                addr.State = applicationUser.StateValue;
                addr.City = applicationUser.CityValue;
                addr.Apartment = applicationUser.ApartmentValue;
                addr.StreetAddress = applicationUser.AddressValue;
                addr.Zip = applicationUser.ZipValue;
                _db.Address.Add(addr);
                objFromDb.Address = addr;

                _db.SaveChanges();

                objFromDb.AddressId = addr.Id;
            }
            else
            {
                _db.Address.Update(addr);
                objFromDb.Address = addr;
            }

            _db.SaveChanges();
        }
    }
}
