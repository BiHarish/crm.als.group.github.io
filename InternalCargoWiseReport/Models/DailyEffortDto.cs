using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalCargoWiseReport.Models
{
    public class DailyEffortDto
    {
        public int? sed_id { get; set; }
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? sed_om_id { get; set; }
        public DateTime? sed_requestdate { get; set; }
        public string sed_requestedby { get; set; }
        public string Application { get; set; }
        public string sed_applicationmodule { get; set; }
        public string sed_businessjustification { get; set; }
        public string sed_effortestimate { get; set; }
        public string sed_approvedby { get; set; }
        public long? sed_createdby { get; set; }
        public DateTime? sed_createdon { get; set; }
        public string sed_filename { get; set; }
        public int? sed_modifiedby { get; set; }
        public DateTime? modifiedon { get; set; }
        public string sed_effortcreatedby { get; set; }
    }
}