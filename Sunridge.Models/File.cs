using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class File
    {
        public int FileId { get; set; }
        public int LotId { get; set; }

        public string FileURL { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("LotId")]
        public Lot Lot { get; set; }
        public bool IsArchive { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
