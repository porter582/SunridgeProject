using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Classifieds
{
    public class LotsModel : PageModel
    {
        public readonly IUnitOfWork _unitOfWork;


        public LotsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ClassifiedListing> ClassifiedListingsList { get; set; }
        [BindProperty]
        public List<ClassifiedListing> ClassifiedListings { get; set; }

        public void OnGet()
        {
            ClassifiedListingsList = _unitOfWork.ClassifiedListing.GetAll();
            ClassifiedImage temp = new ClassifiedImage();
            ClassifiedListings = new List<ClassifiedListing>();
            foreach (var listing in ClassifiedListingsList)
            {
                IEnumerable<ClassifiedImage> tempList = _unitOfWork.ClassifiedImage.GetAll(u => u.ClassifiedListingId == listing.ClassifiedListingId);

                listing.Images = tempList.ToList();
                ClassifiedListings.Add(listing);
            }
        }
    }
}