using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class Key
    {
        public int KeyId { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        [Range(2000, 2030)]
        public int Year { get; set; }
        public bool IsArchive { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        [NotMapped]
        public string FullSerial
        {
            get
            {
                return $"{Year}-{SerialNumber}";
            }
        }
    }
}
