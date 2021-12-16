using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class SchemaMasterDto
    {
        public int SrNo { get; set; }
        public int SchemaMasterId { get; set; }
        public string SchemaMasterName { get; set; }
        public bool SchemaMasterIsActive { get; set; }
    }
}