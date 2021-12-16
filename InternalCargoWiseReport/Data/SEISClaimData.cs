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
    public class SEISClaimData
    {
        SqlParameter[] Param(SEISClaimDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[26];
            Param[0] = new SqlParameter("@srid", SqlDbType.BigInt);
            Param[0].Value = obj.srid;
            Param[1] = new SqlParameter("@srdivisionid", SqlDbType.BigInt);
            Param[1].Value = obj.srdivisionid;
            Param[2] = new SqlParameter("@srmpmid", SqlDbType.BigInt);
            Param[2].Value = obj.srmpmid;
            Param[3] = new SqlParameter("@srfinancialyear", SqlDbType.NVarChar);
            Param[3].Value = obj.srfinancialyear;
            Param[4] = new SqlParameter("@srApr", SqlDbType.Float);
            Param[4].Value = obj.srApr;
            Param[5] = new SqlParameter("@srMay", SqlDbType.Float);
            Param[5].Value = obj.srMay;
            Param[6] = new SqlParameter("@srJun", SqlDbType.Float);
            Param[6].Value = obj.srJun;
            Param[7] = new SqlParameter("@srJul", SqlDbType.Float);
            Param[7].Value = obj.srJul;
            Param[8] = new SqlParameter("@srAug", SqlDbType.Float);
            Param[8].Value = obj.srAug;
            Param[9] = new SqlParameter("@srSep", SqlDbType.Float);
            Param[9].Value = obj.srSep;
            Param[10] = new SqlParameter("@srOct", SqlDbType.Float);
            Param[10].Value = obj.srOct;
            Param[11] = new SqlParameter("@srNov", SqlDbType.Float);
            Param[11].Value = obj.srNov;
            Param[12] = new SqlParameter("@srDec", SqlDbType.Float);
            Param[12].Value = obj.srDec;
            Param[13] = new SqlParameter("@srJan", SqlDbType.Float);
            Param[13].Value = obj.srJan;
            Param[14] = new SqlParameter("@srFeb", SqlDbType.Float);
            Param[14].Value = obj.srFeb;
            Param[15] = new SqlParameter("@srMar", SqlDbType.Float);
            Param[15].Value = obj.srMar;
            Param[16] = new SqlParameter("@srTotal", SqlDbType.Float);
            Param[16].Value = obj.srTotal;
            Param[17] = new SqlParameter("@srcreateby", SqlDbType.BigInt);
            Param[17].Value = obj.srcreateby;
            Param[18] = new SqlParameter("@msrrid", SqlDbType.BigInt);
            Param[18].Value = obj.msrrid;
            Param[19] = new SqlParameter("@msrrMonth", SqlDbType.Float);
            Param[19].Value = obj.msrrMonth;
            Param[20] = new SqlParameter("@msrrMonthName", SqlDbType.NVarChar);
            Param[20].Value = obj.msrrMonthName;
            Param[21] = new SqlParameter("@msrrRemarks", SqlDbType.NVarChar);
            Param[21].Value = obj.msrrRemarks;
            Param[22] = new SqlParameter("@msrrvTotal", SqlDbType.Float);
            Param[22].Value = obj.msrrvTotal;
            Param[23] = new SqlParameter("@msrrRemarkYtd", SqlDbType.BigInt);
            Param[23].Value = obj.msrrRemarkYtd;
            Param[24] = new SqlParameter("@msrrmtyid", SqlDbType.NVarChar);
            Param[24].Value = obj.msrrmtyid;
            Param[25] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[25].Value = flag;

            return Param;
        }

        public string Saveseisdata(SEISClaimDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspmisseisclaim", Params);

                    if (i != null)
                    {
                        return i.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
                finally
                {

                }
            }
            return string.Empty;
        }
        public DataSet GetClaimdata(SEISClaimDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisSeisClaim", Params);

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

        public DataSet BindDivision(SEISClaimDto obj)
        {
            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisseisclaim", Params);

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

        public DataSet BindMIStype(SEISClaimDto obj)
        {
            var Params = Param(obj, 4);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisseisclaim", Params);

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
        public DataSet getDataFromParticularMaster(SEISClaimDto mis)
        {
            var Params = Param(mis, 5);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisSeisClaim", Params);

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

        public DataSet GetMisdatabyFYearandDivision(SEISClaimDto obj)
        {
            var Params = Param(obj, 6);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisseisclaim", Params);

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