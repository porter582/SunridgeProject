using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Admin.Reports
{
    public class ClaimReportUpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ClaimReportUpsertModel(IUnitOfWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }

        //binds the model to the page
        [BindProperty]
        public PropDamageClaimVM ClaimReportObj { get; set; }
        public IActionResult OnGet(int? id) ///IActionResult return type is page, obj
        {
            ClaimReportObj = new PropDamageClaimVM()
            {
                claimReport = new PropDamageClaimReport(),
                AdminComments = new AdminComments()
            };
            if (id != null) //edit
            {
                ClaimReportObj.claimReport = _unitofWork.PropDamageClaimReport.GetFirstOrDefault(u => u.id == id);
                try
                {
                    ClaimReportObj.AdminComments = _unitofWork.AdminComments.GetFirstOrDefault(u => u.ReportId == id);
                }
                catch (Exception e)
                {
                    ClaimReportObj.AdminComments = new AdminComments();
                }
                if (ClaimReportObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            //find root path wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
            //Grab the file(s) from the form
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ClaimReportObj.claimReport.id == 0) //new item
            {
                try
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                    if (claim != null)
                    {
                        ClaimReportObj.claimReport.ApplicationUserId = claim.Value;
                        ClaimReportObj.claimReport.ApplicationUser = _unitofWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                        ClaimReportObj.claimReport.username = ClaimReportObj.claimReport.ApplicationUser.FullName;

                    }
                    if (ClaimReportObj.claimReport.resolved == true)
                    {
                        ClaimReportObj.claimReport.resolveddate = DateTime.Today.ToString();
                    }
                    else
                    {
                        ClaimReportObj.claimReport.resolveddate = "Unresolved";
                    }
                    ClaimReportObj.claimReport.listingDate = DateTime.Now.ToString();
                    //rename file user submits for image
                    string fileName = Guid.NewGuid().ToString();
                    ClaimReportObj.claimReport.FileName = files[0].FileName;
                    //upload file to the path
                    var uploads = Path.Combine(webRootPath, @"images\claimreports");
                    //preserve our extension

                    if (files.Count > 0)
                    {
                        var extension = Path.GetExtension(files[0].FileName);

                        using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(filestream); //files variable comes from the razor page files id
                        }

                        ClaimReportObj.claimReport.FilePath = @"\images\claimreports\" + fileName + extension;
                    }
                }
                catch (Exception e)
                {

                }

                _unitofWork.PropDamageClaimReport.Add(ClaimReportObj.claimReport);
            }
            else //else we edit
            {
                var objFromDb =
                    _unitofWork.PropDamageClaimReport.Get(ClaimReportObj.claimReport.id);
                ClaimReportObj.claimReport.listingDate = objFromDb.listingDate;
                if (ClaimReportObj.AdminComments != null)
                {
                    ClaimReportObj.AdminComments.ReportId = ClaimReportObj.claimReport.id;
                }
                //checks if there are files submitted
                if (files.Count > 0)
                {
                    //rename the file the user submits
                    //guid is a unique string that will not be duplicated
                    string fileName = Guid.NewGuid().ToString();

                    //upload to path
                    var uploads = Path.Combine(webRootPath, @"images\claimreports");

                    //preserve our extension
                    var extension = Path.GetExtension(files[0].FileName);
                    var imagePath = Path.Combine(webRootPath, objFromDb.FilePath.TrimStart('\\'));

                    //if image already exists
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    //append new name to the extension
                    using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }

                    ClaimReportObj.claimReport.FilePath = @"\images\claimreports\" + fileName + extension;
                }
                else
                {
                    ClaimReportObj.claimReport.FilePath = objFromDb.FilePath;
                    ClaimReportObj.claimReport.FileName = objFromDb.FileName;
                }
                try
                {
                    if (ClaimReportObj.AdminComments != null)
                    {
                        var claimsIdentity = (ClaimsIdentity)User.Identity;
                        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                        ClaimReportObj.AdminComments.ApplicationUserId = claim.Value;
                        ClaimReportObj.AdminComments.ApplicationUser = _unitofWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                        _unitofWork.AdminComments.Update(ClaimReportObj.AdminComments);
                    }
                }catch(Exception e)
                {
                    if(ClaimReportObj.AdminComments != null)
                    {
                        var claimsIdentity = (ClaimsIdentity)User.Identity;
                        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                        ClaimReportObj.AdminComments.ApplicationUserId = claim.Value;
                        ClaimReportObj.AdminComments.ApplicationUser = _unitofWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                        _unitofWork.AdminComments.Add(ClaimReportObj.AdminComments);
                    }
                }
                if (ClaimReportObj.claimReport.resolved == true)
                {
                    ClaimReportObj.claimReport.resolveddate = DateTime.Today.ToString();
                }
                else
                {
                    ClaimReportObj.claimReport.resolveddate = "Unresolved";
                }
                ClaimReportObj.claimReport.ApplicationUser = objFromDb.ApplicationUser;
                ClaimReportObj.claimReport.ApplicationUserId = objFromDb.ApplicationUserId;
                ClaimReportObj.claimReport.username = objFromDb.username;
                _unitofWork.PropDamageClaimReport.Update(ClaimReportObj.claimReport);
            }
            _unitofWork.Save();
            return RedirectToPage("./ClaimReportIndex");
        }
    }
}