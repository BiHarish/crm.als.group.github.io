using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class StateDto
    {
        public int SrNo { get; set; }
        public long? StateId { get; set; }
        public string StateName { get; set; }
        public int? StateCountryId { get; set; }
        public bool StateIsActive { get; set; }
        public string CountryName { get; set; }
    }
}