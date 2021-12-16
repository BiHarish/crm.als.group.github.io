using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class CountryDto
    {
        public int SrNo { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public bool CountryIsActive { get; set; }
    }
}