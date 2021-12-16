using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ICWR.Data
{
    public class AuditTrailMasterData
    {
        SqlParameter[] Param(AuditTrailMasterDto obj, int flag)
        { 

            SqlParameter[] Param = new SqlParameter[8];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt); 
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@ActionHeader", SqlDbType.NVarChar);
            Param[1].Value = obj.ActionHeader;

            Param[2] = new SqlParameter("@ActionName", SqlDbType.NVarChar);
            Param[2].Value = obj.ActionName;

            Param[3] = new SqlParameter("@UserID", SqlDbType.BigInt);
            Param[3].Value = LovelySession.Lovely.User.Id;

            Param[4] = new SqlParameter("@Remarks", SqlDbType.NVarChar);
            Param[4].Value = obj.Remarks;

            Param[5] = new SqlParameter("@flag", SqlDbType.BigInt);
            Param[5].Value = flag ;
            Param[6] = new SqlParameter("@ActionFromDate", SqlDbType.DateTime); 
            Param[6].Value = obj.ActionFromDate;
            Param[7] = new SqlParameter("@ActionToDate", SqlDbType.DateTime);
            Param[7].Value = obj.ActionToDate;

         

          


            return Param;
        }

        public long Insert(AuditTrailMasterDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspAudit_Trail_Log", Params);

                    if (i != null)
                    {
                        return Convert.ToInt64(i);
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
                finally
                {

                }
            }
            return -1;
        }

        public IList<AuditTrailMasterDto> getByFromAndToDate(AuditTrailMasterDto obj)
        {
            DataSet _ds = null;
           
            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspAudit_Trail_Log", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<AuditTrailMasterDto> results = new List<AuditTrailMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            AuditTrailMasterDto Obj = new AuditTrailMasterDto();

                            Obj.ActionDate = row["ActionDate"].ToDataConvertDateTime();
                            Obj.ActionHeader = row["ActionHeader"].ToString();
                            Obj.ActionName = row["ActionName"].ToString();
                            Obj.Remarks = row["Remarks"].ToString();
                            Obj.UserName = row["UserName"].ToString();

                            results.Add(Obj);
                        }
                        return results;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

    }
}