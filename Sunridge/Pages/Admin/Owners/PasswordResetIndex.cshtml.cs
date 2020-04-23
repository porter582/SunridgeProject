using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Admin.Owners
{
    public class PasswordResetIndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public ApplicationUser Owner { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string passwordConfirmation { get; set; }

        public PasswordResetIndexModel(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public IActionResult OnGet(string id)
        {
            Owner = _unitOfWork.ApplicationUser.GetFirstOrDefault(s => s.Id == id);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (Owner.Id == null || Owner.Id == "")
            {
                return Page();
            }

            //ApplicationUser temp = new ApplicationUser();

            var temp = await _userManager.FindByIdAsync(Owner.Id);

            //temp = _unitOfWork.ApplicationUser.GetFirstOrDefault(s => s.Id == Owner.Id);

            temp.UserName = Owner.UserName;
            temp.Email = Owner.UserName;
            //var result = await _userManager.ChangePasswordAsync(temp, currrentPassword, newPassword);
            string token = await _userManager.GeneratePasswordResetTokenAsync(temp);
            var result = await _userManager.ResetPasswordAsync(temp, token, password);

            //temp.PasswordHash = _userManager.PasswordHasher.HashPassword(temp, password);
            //var result = await _userManager.UpdateAsync(temp);

            if (!result.Succeeded)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}