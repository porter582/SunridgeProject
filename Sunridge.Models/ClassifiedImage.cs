using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models
{
    public class ClassifiedImage
    {
        public int ClassifiedImageId { get; set; }
        public int ClassifiedListingId { get; set; }

        public bool IsMainImage { get; set; }
        public string ImageURL { get; set; }
        public string ImageExtension { get; set; }
    }
}
