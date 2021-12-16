using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class RoleDto
    {
        public int SrNo { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public bool RoleIsActive { get; set; }
        public string RoleAmount { get; set; }
    }
}