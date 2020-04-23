using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsArchive { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }
}
