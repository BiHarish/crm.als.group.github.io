using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalCargoWiseReport.Models
{
    public class WHMTransDto
    {
        public long ID{ get; set; }
        public string CustomerName { get; set; }
        public long?WHMID{ get; set; }
        public double?TotArea{ get; set; }
        public double?OccupiedArea{ get; set; }
        public double?Rate{ get; set; }
        public double?Vacant { get; set; }

        //Extra Fields

        public string LocationName { get; set; }
    }
}