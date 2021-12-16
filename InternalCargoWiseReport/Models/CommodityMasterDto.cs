using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class CommodityMasterDto
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public long? ModifyBy { get; set; }
        public DateTime? ModifyOn { get; set; }
    }
}