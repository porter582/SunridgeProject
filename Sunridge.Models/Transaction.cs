using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int LotId { get; set; }
        public string OwnerId { get; set; }
        public int TransactionTypeId { get; set; }

        public string Description { get; set; }
        public float Amount { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DatePaid { get; set; }
        public string Status { get; set; }
        public bool IsArchive { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        //Nav properties
        [ForeignKey("LotId")]
        public Lot Lot { get; set; }
        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; }
        [ForeignKey("TransactionTypeId")]
        public TransactionType TransactionType { get; set; }

    }
}
