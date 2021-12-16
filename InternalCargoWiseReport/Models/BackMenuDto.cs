using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class BackMenuDto
    {
        public int BackMenuId { get; set; }
        public string BackMenuName { get; set; }
        public int? BackMenuParentId { get; set; }
        public string BackMenuURL { get; set; }

        public List<BackMenuDto> BackMenuChild { get; set; }
        public BackMenuDto BackMenuParent { get; set; } 
    }
}