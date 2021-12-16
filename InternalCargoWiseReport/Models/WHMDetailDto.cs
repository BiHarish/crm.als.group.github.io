using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalCargoWiseReport.Models
{
    public class WHMDetailDto
    {

        public long? ID { get; set; }
        public long? LocationID { get; set; }
        public double? TotalArea { get; set; }
        public double? AreaUtilised { get; set; }
        public double? AreaVacant { get; set; }
        public double? Rate { get; set; }
        public long CreateBy { get; set; }

        //Extra Field
        public string Location { get; set; }

    }
}