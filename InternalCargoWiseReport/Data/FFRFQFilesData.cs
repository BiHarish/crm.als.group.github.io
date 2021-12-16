using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class FFRFQFilesData
    {
        SqlParameter[] Param(FFRFQFilesDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@WhLeadID", SqlDbType.BigInt);
            Param[1].Value = obj.WhLeadID;

            Param[2] = new SqlParameter("@FileName", SqlDbType.NVarChar);
            Param[2].Value = obj.FileName;

            Param[3] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[3].Value = obj.CreateBy;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;

            return Param;
        }
        public bool Insert(FFRFQFilesDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspFFRFQFiles", Params);

                    if (i > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {

                }
            }
            return false;
        }
        //public bool Update(FFRFQFilesDto obj)
        //{
        //    var Params = Param(obj, 2);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspCity", Params);

        //            if (i > 0)
        //            {
        //                return true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    return false;
        //}
        public IList<FFRFQFilesDto> GetByWhLeadId(long? WhLeadID)
        {
            DataSet _ds = null;

            FFRFQFilesDto _FFData = new FFRFQFilesDto();
            _FFData.WhLeadID = WhLeadID;
            var Params = Param(_FFData, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspFFRFQFiles", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<FFRFQFilesDto> results = new List<FFRFQFilesDto>();

                        foreach (DataRow row in rows)
                        {
                            FFRFQFilesDto result = new FFRFQFilesDto();

                            result.ID = row["ID"].ToDataConvertInt64();
                            result.WhLeadID = row["WhLeadID"].ToDataConvertNullInt64();
                            result.FileName = row["FileName"].ToString();
                            result.CreateDate = row["CreateOn"].ToDataConvertNullDateTime();

                            results.Add(result);
                        }
                       
                        return results;
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