using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi
{
    public class BlobInformation
    {
        public Uri BlobUri { get; set; }

        public string BlobName
        {
            get
            {
                return BlobUri.Segments[BlobUri.Segments.Length - 1];
            }
        }
        public string BlobNameWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(BlobName);
            }
        }
        public int AdId { get; set; }
    }
}