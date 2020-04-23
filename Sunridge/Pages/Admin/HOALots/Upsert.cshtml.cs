using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Admin.HOALots
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        //binds the model to the page
        [BindProperty]
        public Lot LotObj { get; set; }
        [BindProperty]
        public Address AddressObj { get; set; }
        [BindProperty]
        public IEnumerable<SelectListItem> primaryOwners { get; set; }
        [BindProperty]
        public IEnumerable<SelectListItem> secondaryOwners { get; set; }
        [BindProperty]
        public string PrimaryOwner { get; set; }
        [BindProperty]
        public string SecondaryOwner { get; set; }
        [BindProperty]
        public IEnumerable<Inventory> InventoryObj { get; set; }
        [BindProperty]
        public string[] InventoryItems { get; set; } 
        [BindProperty]
        public bool[] InventoryCheckboxes { get; set; }
        [BindProperty]
        public string[] CheckboxesChecked { get; set; }
        [BindProperty]
        public OwnerLot OwnerLotObj { get; set; }

        public UpsertModel(IUnitOfWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet(int id)
        {
            LotObj = new Lot();
            LotObj = _unitofWork.Lot.GetFirstOrDefault(u => u.LotId == id);

            AddressObj = _unitofWork.Address.GetFirstOrDefault(s => s.Id == LotObj.AddressId);
     
            PrimaryOwner = "";
            var lotOwner = _unitofWork.OwnerLot.GetFirstOrDefault(s => s.LotId == LotObj.LotId && s.IsPrimary == true);
            if (lotOwner != null)
            {
                var primaryOwner = _unitofWork.ApplicationUser.GetFirstOrDefault(s => s.Id == lotOwner.OwnerId);
                if (primaryOwner != null)
                {
                    primaryOwners = _unitofWork.ApplicationUser.GetApplicationUserListOrDropdown(primaryOwner.Id);
                    foreach (var item in primaryOwners)
                    {
                        if (item.Selected == true)
                            PrimaryOwner = item.Value;
                    }
                }
                else
                {
                    primaryOwners = _unitofWork.ApplicationUser.GetApplicationUserListOrDropdown();
                }
            }
            else
            {
                primaryOwners = _unitofWork.ApplicationUser.GetApplicationUserListOrDropdown();
            }

            SecondaryOwner = "";
            lotOwner = _unitofWork.OwnerLot.GetFirstOrDefault(s => s.LotId == LotObj.LotId && s.IsPrimary == false);
            if (lotOwner != null)
            {
                var secondaryOwner = _unitofWork.ApplicationUser.GetFirstOrDefault(s => s.Id == lotOwner.OwnerId);
                if (secondaryOwner != null)
                {
                    secondaryOwners = _unitofWork.ApplicationUser.GetApplicationUserListOrDropdown(secondaryOwner.Id);
                    foreach (var item in secondaryOwners)
                    {
                        if (item.Selected == true)
                            SecondaryOwner = item.Value;
                    }
                }
                else
                {
                    secondaryOwners = _unitofWork.ApplicationUser.GetApplicationUserListOrDropdown();
                }
            }
            else
            {
                secondaryOwners = _unitofWork.ApplicationUser.GetApplicationUserListOrDropdown();
            }


            InventoryObj = _unitofWork.Inventory.GetAll(null, null, null);

            InventoryCheckboxes = new bool[InventoryObj.Count()];
            CheckboxesChecked = new string[InventoryObj.Count()];

            InventoryItems = new string[InventoryObj.Count()];

            int count = 0;
            foreach (var item in InventoryObj)
            {
                InventoryItems[count] = item.Description;


                var invItems = _unitofWork.LotInventory.GetAll(s => s.LotId == LotObj.LotId);
                foreach( var invIt in invItems)
                {
                    var obj = _unitofWork.Inventory.GetFirstOrDefault(s => s.Description == item.Description && s.InventoryId == invIt.InventoryId);
                    if (obj != null && obj.Description == item.Description)
                        CheckboxesChecked[count] = "checked";
                }

                count++;
            }
                 
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //update lot
            var lot = _unitofWork.Lot.GetFirstOrDefault(s => s.LotId == LotObj.LotId);
            lot.LotNumber = LotObj.LotNumber;
            lot.TaxId = LotObj.TaxId;
            _unitofWork.Lot.Update(lot);
            _unitofWork.Save();

            //update address
            var addr = _unitofWork.Address.GetFirstOrDefault(s => s.Id == AddressObj.Id);
            addr.StreetAddress = AddressObj.StreetAddress;
            addr.Apartment = AddressObj.Apartment;
            addr.City = AddressObj.City;
            addr.State = AddressObj.State;
            addr.Zip = AddressObj.Zip;
            _unitofWork.Address.Update(addr);
            _unitofWork.Save();

            //add/update ownerlot
            if (PrimaryOwner != null)
            {
                var ownerLot = _unitofWork.OwnerLot.GetFirstOrDefault(s => s.LotId == LotObj.LotId && s.IsPrimary == true);
                if (ownerLot != null)
                {
                    ownerLot.OwnerId = PrimaryOwner;
                    _unitofWork.OwnerLot.Update(ownerLot);
                    _unitofWork.Save();
                }
                else
                {
                    ownerLot = new OwnerLot();
                    ownerLot.LotId = LotObj.LotId;
                    ownerLot.OwnerId = PrimaryOwner;
                    ownerLot.IsPrimary = true;
                    ownerLot.StartDate = DateTime.Now;
                    ownerLot.EndDate = DateTime.Now.AddYears(200);
                    ownerLot.LastModifiedDate = DateTime.Now;
                    _unitofWork.OwnerLot.Add(ownerLot);
                    _unitofWork.Save();
                }
            }

            if (SecondaryOwner != null)
            {
                var ownerLot = _unitofWork.OwnerLot.GetFirstOrDefault(s => s.LotId == LotObj.LotId && s.IsPrimary == false);
                if (ownerLot != null)
                {
                    ownerLot.OwnerId = SecondaryOwner;
                    _unitofWork.OwnerLot.Update(ownerLot);
                    _unitofWork.Save();
                }
                else
                {
                    ownerLot = new OwnerLot();
                    ownerLot.LotId = LotObj.LotId;
                    ownerLot.OwnerId = SecondaryOwner;
                    ownerLot.IsPrimary = false;
                    ownerLot.StartDate = DateTime.Now;
                    ownerLot.EndDate = DateTime.Now.AddYears(200);
                    ownerLot.LastModifiedDate = DateTime.Now;
                    _unitofWork.OwnerLot.Add(ownerLot);
                    _unitofWork.Save();
                }
            }

            //update lot inventory
            var lotInventories = _unitofWork.LotInventory.GetAll(s => s.LotId == LotObj.LotId);
            foreach(var lotInv in lotInventories)
            {
                _unitofWork.LotInventory.Remove(lotInv);
                _unitofWork.Save();
            }

            var invItems = _unitofWork.Inventory.GetAll(null, null, null);
            int count = 0;
            foreach(var invItem in invItems)
            {
                if (InventoryCheckboxes[count] == true)
                {
                    LotInventory lotInventory = new LotInventory();
                    lotInventory.LotId = LotObj.LotId;
                    lotInventory.InventoryId = invItem.InventoryId;
                    lotInventory.IsArchive = false;
                    lotInventory.LastModifiedDate = DateTime.Now;

                    _unitofWork.LotInventory.Add(lotInventory);
                    _unitofWork.Save();
                }
                count++;
            }

            return RedirectToPage("./Index");
        }
    }
}