using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class GeneratedIncentivesData
    {
        SqlParameter[] Param(GeneratedIncentivesDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[4];
            Param[0] = new SqlParameter("@AssessmentYear", SqlDbType.NVarChar);
            Param[0].Value = obj.AssessmentYear;

            Param[1] = new SqlParameter("@CargoCode", SqlDbType.NVarChar);
            Param[1].Value = obj.CargoCode;

            Param[2] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[2].Value = obj.Name;
           
            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            return Param;
        }
        //public bool Insert(GeneratedIncentivesDto obj)
        //{
        //    var Params = Param(obj, 1);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspGeneratedIncentives", Params);

        //            if (i > 0)
        //            {
        //                return true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    return false;
        //}
        //public bool Update(GeneratedIncentivesDto obj)
        //{
        //    var Params = Param(obj, 2);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspGeneratedIncentives", Params);

        //            if (i > 0)
        //            {
        //                return true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    return false;
        //}
        public GeneratedIncentivesDto getIncentiveData(string FinYear,string Name)
        {
            DataSet _ds = null;

            GeneratedIncentivesDto _GeneratedIncentives = new GeneratedIncentivesDto();
            _GeneratedIncentives.AssessmentYear = FinYear;
            _GeneratedIncentives.Name = Name;

            var Params = Param(_GeneratedIncentives, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspGeneratedIncentives", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        GeneratedIncentivesDto result = new GeneratedIncentivesDto();
                        {
                            //result.GeneratedIncentivesId = row["GeneratedIncentivesId"] == DBNull.Value ? 0 : Convert.ToInt32(row["GeneratedIncentivesId"]);
                            //result.GeneratedIncentivesName = row["GeneratedIncentivesName"].ToString();
                            //result.GeneratedIncentivesParentId = row["GeneratedIncentivesParentId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["GeneratedIncentivesParentId"]);
                        }
                        return result;
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
        //public IList<GeneratedIncentivesDto> GetAll()
        //{
        //    DataSet _ds = null;
        //    var Params = Param(new GeneratedIncentivesDto(), 4);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspGeneratedIncentives", Params);

        //            if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRowCollection rows = _ds.Tables[0].Rows;
        //                IList<GeneratedIncentivesDto> results = new List<GeneratedIncentivesDto>();

        //                foreach (DataRow row in rows)
        //                {
        //                    GeneratedIncentivesDto obj = new GeneratedIncentivesDto();

        //                    obj.GeneratedIncentivesId = row["GeneratedIncentivesId"] == DBNull.Value ? 0 : Convert.ToInt32(row["GeneratedIncentivesId"]);
        //                    obj.GeneratedIncentivesName = row["GeneratedIncentivesName"].ToString();
        //                    obj.GeneratedIncentivesParentId = row["GeneratedIncentivesParentId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["GeneratedIncentivesParentId"]);
        //                    obj.GeneratedIncentivesURL = row["GeneratedIncentivesURL"].ToString();
        //                    results.Add(obj);
        //                }
        //                return results;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //    return null;
        //}
    }
}
