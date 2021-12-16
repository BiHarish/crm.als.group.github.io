using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InternalCargoWiseReport.Models;

namespace ICWR.Data
{
    public class UpdateYearIntrestData
    {
        SqlParameter[] Param(UpdateYearIntrestDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[7];
            Param[0] = new SqlParameter("@IAM_Id", SqlDbType.BigInt);
            Param[0].Value = obj.IAM_Id;

            Param[1] = new SqlParameter("@IAM_CWPeriod", SqlDbType.NVarChar);
            Param[1].Value = obj.IAM_CWPeriod;

            Param[2] = new SqlParameter("@IAM_SelfPeriod", SqlDbType.NVarChar);
            Param[2].Value = obj.IAM_SelfPeriod;

            Param[3] = new SqlParameter("@IAM_StartDate", SqlDbType.DateTime);
            Param[3].Value = obj.IAM_StartDate;

            Param[4] = new SqlParameter("@IAM_EndDate", SqlDbType.DateTime);
            Param[4].Value = obj.IAM_EndDate;

            Param[5] = new SqlParameter("@IAM_PercentOfInterest", SqlDbType.Float);
            Param[5].Value = obj.IAM_PercentOfInterest;

            Param[6] = new SqlParameter("@flag", SqlDbType.Float);
            Param[6].Value = flag;


            return Param;
        }
        public bool Update(UpdateYearIntrestDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspUpdateIntrestmanage", Params);

                    if (i > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                }
            }
            return false;
        }
        public IList<UpdateYearIntrestDto> GetByYear(string YearID)
        {
            DataSet _ds = null;
            UpdateYearIntrestDto _UpdateYearIntrestDto = new UpdateYearIntrestDto();
            _UpdateYearIntrestDto.IAM_SelfPeriod=YearID;
            
            var Params = Param(_UpdateYearIntrestDto, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspUpdateIntrestmanage", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UpdateYearIntrestDto> results = new List<UpdateYearIntrestDto>();
                        foreach (DataRow row in rows)
                        {
                            UpdateYearIntrestDto obj = new UpdateYearIntrestDto();

                            obj.IAM_Id = row["IAM_Id"] == DBNull.Value ? 0 : Convert.ToInt32(row["IAM_Id"]);
                            obj.IAM_CWPeriod = row["IAM_CWPeriod"].ToString();
                            obj.IAM_SelfPeriod = row["IAM_SelfPeriod"].ToString();
                            obj.IAM_StartDate = row["IAM_StartDate"].ToDataConvertNullDateTime();
                            obj.IAM_EndDate = row["IAM_EndDate"].ToDataConvertNullDateTime();
                            obj.IAM_PercentOfInterest = row["IAM_PercentOfInterest"].ToDataConvertNullDouble();
                            results.Add(obj);
                        }
                        
                        return results;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                }
            }
            return null;
        }

    }
}