using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class WhDocumentUploadTransDto
    {
        public long? ID { get; set; }
        public string WhLeadID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public  long? CreateBy { get; set; }

    }

    public class WhPostNegotitationStageDto
    {
        public long? ID { get; set; }
        public string Stage { get; set; }
    }
}