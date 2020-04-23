using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int AddressId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public string Occupation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}",
            ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Emergency Contact")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public string EmergencyContactName { get; set; }

        [Display(Name = "Emergency Contact #")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public string EmergencyContactPhone { get; set; }

        [Display(Name = "Receive Sunridge emails")]
        public bool? ReceiveEmails { get; set; }

        [Display(Name = "Archived")]
        public bool IsArchive { get; set; } = false;

        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        //Navigation properties
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        public virtual ICollection<OwnerLot> OwnerLots { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<FormResponse> FormResponses { get; set; }
        public virtual ICollection<ClassifiedListing> ClassifiedListings { get; set; }
        public virtual ICollection<KeyHistory> KeyHistories { get; set; }
        public virtual ICollection<LostAndFoundItem> LostAndFoundItems { get; set; }



        [NotMapped]
        public string ApartmentValue { get; set; }
        [NotMapped]
        public string CityValue { get; set; }
        [NotMapped]
        public string StateValue { get; set; }
        [NotMapped]
        public string ZipValue { get; set; }
        [NotMapped]
        public string AddressValue { get; set; }
        // Calculated properties
        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
