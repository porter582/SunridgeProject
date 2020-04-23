using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Owners
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        //binds the model to the page
        [BindProperty]
        public ApplicationUser OwnerObj { get; set; }
        [BindProperty]
        public Address AddressObj { get; set; }
        [BindProperty]
        public bool createAnother { get; set; }
        [BindProperty]
        public bool redirect { get; set; }
        [BindProperty]
        [DisplayName("User Type")]
        public string OwnerType { get; set; }

        public UpsertModel(IUnitOfWork unitofWork, IWebHostEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        public IActionResult OnGet(string id, bool New = false)
        {
            if (New)
            {
                OwnerObj = null;
                OwnerObj = new ApplicationUser();

                var temp = _unitofWork.ApplicationUser.GetFirstOrDefault(s => s.Id == id);

                OwnerObj.AddressId = temp.AddressId;
                OwnerObj.EmergencyContactName = temp.EmergencyContactName;
                OwnerObj.EmergencyContactPhone = temp.EmergencyContactPhone;
                
                var tempAddr = _unitofWork.Address.GetFirstOrDefault(u => u.Id == OwnerObj.AddressId);

                AddressObj = null;
                AddressObj = new Address();

                AddressObj.StreetAddress = tempAddr.StreetAddress;
                AddressObj.Apartment = tempAddr.Apartment;
                AddressObj.City = tempAddr.City;
                AddressObj.State = tempAddr.State;
                AddressObj.Zip = tempAddr.Zip;
            }
            else
            {
                OwnerObj = new ApplicationUser();
                AddressObj = new Address();

                if (id != null)
                    OwnerObj = _unitofWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);

                if (id != null)
                    AddressObj = _unitofWork.Address.GetFirstOrDefault(u => u.Id == OwnerObj.AddressId);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (AddressObj.Id != 0)
                OwnerObj.AddressId = AddressObj.Id;

            var user = _unitofWork.ApplicationUser.GetFirstOrDefault(s => s.Id == OwnerObj.Id);

            if (user == null)
            {
                _unitofWork.Address.Add(AddressObj);
                OwnerObj.AddressId = AddressObj.Id;
                OwnerObj.Address = AddressObj;
                OwnerObj.UserName = OwnerObj.FirstName + OwnerObj.LastName;
                OwnerObj.UserName = OwnerObj.Email;

                //get generic password hash
               
                _unitofWork.ApplicationUser.Add(OwnerObj);
                _unitofWork.Save();
                var success = AddPassword(OwnerObj, "Temppass123$");
                if (!success.Result)
                    return Page();
            }
            else
            {
                _unitofWork.ApplicationUser.Update(OwnerObj);
                _unitofWork.Save();
            }

            if (createAnother)
            {
                return RedirectToPage("Upsert", "OnGet", new { id = OwnerObj.Id, New = true });
            }
            else if (redirect)
            {
                return RedirectToPage("../HOALots/Index", "OnGet");
            }
            else
            {
                return RedirectToPage("Index");
            }
        }

        public async Task<bool> AddPassword(ApplicationUser user, string pass)
        {
            var result1 = await _userManager.AddPasswordAsync(user, pass);

            if (!result1.Succeeded)
                return false;

            if (OwnerType == "Admin")
            {
                var result2 = await _userManager.AddToRoleAsync(user, SD.AdminRole);

                if (!result2.Succeeded)
                    return false;
            }
            else
            {
                var result2 = await _userManager.AddToRoleAsync(user, SD.OwnerRole);

                if (!result2.Succeeded)
                    return false;
            }

            _unitofWork.ApplicationUser.Update(user);

            return true;
        }
    }
}