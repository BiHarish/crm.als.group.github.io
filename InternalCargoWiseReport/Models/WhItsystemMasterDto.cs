using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class WhItsystemMasterDto
    {
        public long? ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public long CreateBy { get; set; }

    }
}