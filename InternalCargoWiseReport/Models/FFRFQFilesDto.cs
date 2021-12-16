using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class FFRFQFilesDto
    {
        public long ID { get; set; }
        public long? WhLeadID { get; set; }
        public string FileName { get; set; }
        public long? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}