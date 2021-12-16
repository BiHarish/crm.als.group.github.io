using ICWR.Data;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
    public  class Audit_Trail_Log
    {
        public static bool  InsertAuditTrailLog(string ActionHeader, string ActionName, string Remarks)
        {
            try 
            {
                AuditTrailMasterData auditTrailData = new AuditTrailMasterData();
                AuditTrailMasterDto obj = new AuditTrailMasterDto();
                obj.ActionHeader = ActionHeader;
                obj.ActionName = ActionName;
                obj.Remarks = Remarks;

                long result = auditTrailData.Insert(obj);
                if(result != null)
                {
                    return true;
                }
                
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }

            return false;
           
        }

    }
}