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
    public class MISVolumeData
    {
        SqlParameter[] Param(MISVolumeDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[22];
            Param[0] = new SqlParameter("@mvtid", SqlDbType.BigInt);
            Param[0].Value = obj.mvtid;
            Param[1] = new SqlParameter("@mvtdivisionid", SqlDbType.BigInt);
            Param[1].Value = obj.mvtdivisionid;
            Param[2] = new SqlParameter("@mvtvtyid", SqlDbType.BigInt);
            Param[2].Value = obj.mvtvtyid;
            Param[3] = new SqlParameter("@mvtfinancialyear", SqlDbType.NVarChar);
            Param[3].Value = obj.mvtfinancialyear;
            Param[4] = new SqlParameter("@mvtmptid", SqlDbType.BigInt);
            Param[4].Value = obj.mvtmptid;
            Param[5] = new SqlParameter("@mvtApr", SqlDbType.Float);
            Param[5].Value = obj.mvtApr;
            Param[6] = new SqlParameter("@mvtMay", SqlDbType.Float);
            Param[6].Value = obj.mvtMay;
            Param[7] = new SqlParameter("@mvtJun", SqlDbType.Float);
            Param[7].Value = obj.mvtJun;
            Param[8] = new SqlParameter("@mvtJul", SqlDbType.Float);
            Param[8].Value = obj.mvtJul;
            Param[9] = new SqlParameter("@mvtAug", SqlDbType.Float);
            Param[9].Value = obj.mvtAug;
            Param[10] = new SqlParameter("@mvtSep", SqlDbType.Float);
            Param[10].Value = obj.mvtSep;
            Param[11] = new SqlParameter("@mvtOct", SqlDbType.Float);
            Param[11].Value = obj.mvtOct;
            Param[12] = new SqlParameter("@mvtNov", SqlDbType.Float);
            Param[12].Value = obj.mvtNov;
            Param[13] = new SqlParameter("@mvtDec", SqlDbType.Float);
            Param[13].Value = obj.mvtDec;
            Param[14] = new SqlParameter("@mvtJan", SqlDbType.Float);
            Param[14].Value = obj.mvtJan;
            Param[15] = new SqlParameter("@mvtFeb", SqlDbType.Float);
            Param[15].Value = obj.mvtFeb;
            Param[16] = new SqlParameter("@mvtMar", SqlDbType.Float);
            Param[16].Value = obj.mvtMar;
            Param[17] = new SqlParameter("@mvtTotal", SqlDbType.Float);
            Param[17].Value = obj.mvtTotal;
            Param[18] = new SqlParameter("@mvtYTD", SqlDbType.Float);
            Param[18].Value = obj.mvtYTD;
            Param[19] = new SqlParameter("@mvtcreateby", SqlDbType.BigInt);
            Param[19].Value = obj.mvtcreateby;
            Param[20] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[20].Value = flag;
            Param[21] = new SqlParameter("@MonthName", SqlDbType.NVarChar);
            Param[21].Value = obj.MonthName;

            return Param;
        }
        public DataSet getDataFromVolumeTable(MISVolumeDto obj)
        {
            var Params = Param(obj, 5);
            if (Params != null)
            { 
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisvolume", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
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

        public DataSet getDataFromParticularMaster(MISVolumeDto obj)
        {
            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisvolume", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
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

        public long Insert(MISVolumeDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspmisvolume", Params);

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

        public bool Update(MISVolumeDto obj)
        {
            var Params = Param(obj, 4);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspmisvolume", Params);

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

        public DataSet getVolumeList(MISVolumeDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisvolume", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
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