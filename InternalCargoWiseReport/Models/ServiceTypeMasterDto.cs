using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalCargoWiseReport.Models
{
    public class ServiceTypeMasterDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public long? CreateBy { get; set; }
    }

    public class WhBuMasterDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? CreateBy { get; set; }
    }
}