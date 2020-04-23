using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class KeyHistory
    {
        public int KeyHistoryId { get; set; }
        public int KeyId { get; set; }

        [Display(Name = "Lot")]
        public int LotId { get; set; }

        public string Status { get; set; }
        [Display(Name ="Date Issued")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateIssued { get; set; }
        [Display(Name = "Date Returned")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateReturned { get; set; }
        [Display(Name = "Paid Amount")]
        public float PaidAmount { get; set; }
        public bool IsArchive { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }


        //Nav properties
        [ForeignKey("KeyId")]
        public Key Key { get; set; }
        [ForeignKey("LotId")]
        public Lot Lot { get; set; }

    }
}
