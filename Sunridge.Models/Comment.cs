using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int? LotHistoryId { get; set; }
        public int? FormResponseId { get; set; }
        public string OwnerId { get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool Private { get; set; }

        // Nav properties
        [ForeignKey("LotHistoryId")]
        public LotHistory LotHistory { get; set; }
        [ForeignKey("FormResponseId")]
        public FormResponse FormResponse { get; set; }
        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; }

    }
}
