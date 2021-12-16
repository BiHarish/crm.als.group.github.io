using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalCargoWiseReport.Models
{
    public class LocationMasterDto
    {
        public long? Id { get; set; }
        public string LocationName { get; set; }
        public string Region { get; set; }
        public string CreateBy { get; set; }
    }
}