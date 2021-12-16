using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Data
{
    public class FilterClass
    {
        public DateTime? datefrom { get; set; }
        public DateTime? dateto { get; set; }
        public int transfer { get; set; }
        public long UserId { get; set; }
    }
}
