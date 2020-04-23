using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class LotHistory
    {
        public int LotHistoryId { get; set; }
        public int LotId { get; set; }

        public string PrivacyLevel { get; set; }
        public bool IsArchive { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        //Navigation Properties
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }
        //public virtual ICollection<File> Files { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
