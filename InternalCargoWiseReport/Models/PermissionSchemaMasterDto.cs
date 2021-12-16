using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class PermissionSchemaMasterDto
    {
        public int SrNo { get; set; }
        public long PermissionSchemaMasterId { get; set; }
        public long PermissionSchemaSchemaMasterId { get; set; }
        public long PermissionSchemaMasterUserRoleId { get; set; }
        public string PermissionSchemaMasterName { get; set; }
        public bool PermissionSchemaMasterview { get; set; }
        public long SchemaMasterId { get; set; }
        public string SchemaMasterName { get; set; }
    }

    public class CurrencyMasterDto
    {
        public string CurrencyName { get; set; }
        public string CurrencyPrice { get; set; }
        public string CurrencyPrice1 { get; set; }
        public DateTime? Date { get; set; }


        //Properties for currency Excel file Download

       
    }
    public class PermissionSchemaMasterShowDto
    {
        public List<RoleDto> RoleDisplay { get; set; }
        // public List<MainMenuDto> MainMenuDisplay { get; set; }
    }

    public class CurrencyMasterForExcel
    {
        public string Mul_Divide { get; set; }
        public string Currency { get; set; }
        public string ConvertedCurrency { get; set; }
        public DateTime? Date { get; set; }
        public double Value { get; set; }

    }
    
}