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
    public class WhBaseRateApprovalData
    {
        SqlParameter[] Param(WhBaseRateApprovalDto obj, int flag)
        { 

            SqlParameter[] Param = new SqlParameter[7];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt); 
            Param[0].Value = obj.Id;

            Param[1] = new SqlParameter("@WhLeadID", SqlDbType.BigInt);
            Param[1].Value = obj.WhLeadID;

            Param[2] = new SqlParameter("@ApproverID", SqlDbType.BigInt);
            Param[2].Value = obj.ApproverID;

            Param[3] = new SqlParameter("@ApprovedDate", SqlDbType.DateTime);
            Param[3].Value = obj.ApprovedDate;

            Param[4] = new SqlParameter("@ApproverStatus", SqlDbType.NVarChar);
            Param[4].Value = obj.ApproverStatus;
            Param[5] = new SqlParameter("@CreateBy", SqlDbType.BigInt); 
            Param[5].Value = obj.CreateBy;
            Param[6] = new SqlParameter("@flag", SqlDbType.BigInt);
            Param[6].Value = flag;
           

         

          


            return Param;
        }

        public long ApprovedBaseRate(WhBaseRateApprovalDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhBaseRateApproval", Params);

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


        public long DisApprovedBaseRate(WhBaseRateApprovalDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhBaseRateApproval", Params);

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

        public WhBaseRateApprovalDto getBdMailIDByLeadID(long? WhLeadID)
        {
            DataSet _ds = null;

            WhBaseRateApprovalDto ApproverData = new WhBaseRateApprovalDto();
            ApproverData.WhLeadID = WhLeadID;
            var Params = Param(ApproverData, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhBaseRateApproval", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhBaseRateApprovalDto result = new WhBaseRateApprovalDto();
                        {
                            result.BdMailID = row["BdMailID"].ToString();

                        }
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }

        public WhBaseRateApprovalDto getMailStatus(WhBaseRateApprovalDto obj)
        {
            DataSet _ds = null;

            
            
            var Params = Param(obj, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhBaseRateApproval", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhBaseRateApprovalDto result = new WhBaseRateApprovalDto();
                        {
                            result.ApproverStatus = row["ApproverStatus"].ToString();

                        }
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }
    }
}