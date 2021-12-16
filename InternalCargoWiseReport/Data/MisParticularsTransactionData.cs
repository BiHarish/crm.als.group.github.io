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
    public class MisParticularsTransactionData
    {
        SqlParameter[] Param(MisParticularsTransactionDto obj, int flag)
        { 

            SqlParameter[] Param = new SqlParameter[24];
            Param[0] = new SqlParameter("@mptid", SqlDbType.BigInt);
            Param[0].Value = obj.mptid;

            Param[1] = new SqlParameter("@mptdivisionid", SqlDbType.BigInt); 
            Param[1].Value = obj.mptdivisionid;

            Param[2] = new SqlParameter("@mptmtyid", SqlDbType.BigInt);
            Param[2].Value = obj.mptmtyid;

            Param[3] = new SqlParameter("@mptfinancialyear", SqlDbType.NVarChar);
            Param[3].Value = obj.mptfinancialyear ;

            Param[4] = new SqlParameter("@mpmid", SqlDbType.BigInt);
            Param[4].Value = obj.mpmid;

            Param[5] = new SqlParameter("@mptApr", SqlDbType.Float);
            Param[5].Value = obj.mptApr;
            Param[6] = new SqlParameter("@mptMay", SqlDbType.Float);
            Param[6].Value = obj.mptMay;
            Param[7] = new SqlParameter("@mptJun", SqlDbType.Float);
            Param[7].Value = obj.mptJun;

            Param[8] = new SqlParameter("@mptJul", SqlDbType.Float);
            Param[8].Value = obj.mptJul;
            Param[9] = new SqlParameter("@mptAug", SqlDbType.Float);
            Param[9].Value = obj.mptAug;
            Param[10] = new SqlParameter("@mptSep", SqlDbType.Float);
            Param[10].Value = obj.mptSep;
            Param[11] = new SqlParameter("@mptOct", SqlDbType.Float);
            Param[11].Value = obj.mptOct;
            Param[12] = new SqlParameter("@mptNov", SqlDbType.Float);
            Param[12].Value = obj.mptNov;
            Param[13] = new SqlParameter("@mptDec", SqlDbType.Float);
            Param[13].Value = obj.mptDec;
            Param[14] = new SqlParameter("@mptJan", SqlDbType.Float);
            Param[14].Value = obj.mptJan;
            Param[15] = new SqlParameter("@mptFeb", SqlDbType.Float);
            Param[15].Value = obj.mptFeb;
            Param[16] = new SqlParameter("@mptMar", SqlDbType.Float);
            Param[16].Value = obj.mptMar;
            Param[17] = new SqlParameter("@mptTotal", SqlDbType.Float);
            Param[17].Value = obj.mptTotal;
            Param[18] = new SqlParameter("@mptYtd", SqlDbType.Float);
            Param[18].Value = obj.mptYtd;
            Param[19] = new SqlParameter("@mptActualRR", SqlDbType.Float);
            Param[19].Value = obj.mptActualRR;
            Param[20] = new SqlParameter("@mptBaltoAchieve", SqlDbType.Float);
            Param[20].Value = obj.mptBaltoAchieve;
            Param[21] = new SqlParameter("@mptRequiredRR", SqlDbType.Float);
            Param[21].Value = obj.mptRequiredRR;
            Param[22] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[22].Value = flag;
            Param[23] = new SqlParameter("@MonthNo", SqlDbType.BigInt);
            Param[23].Value = obj.MonthNo;


         

          


            return Param;
        }

        SqlParameter[] ParamConsol(ConsoleDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@CYfinancialYear", SqlDbType.NVarChar);
            Param[0].Value = obj.CYfinancialYear;

            Param[1] = new SqlParameter("@month", SqlDbType.BigInt);
            Param[1].Value = obj.month;
            Param[2] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[2].Value = flag;




            return Param;
        }

        public DataSet getData(MisParticularsTransactionDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisParticularsTransaction", Params);

                    if (result != null)
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

        public DataSet getDataWithoutSeis(MisParticularsTransactionDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisParticularsTransactionSEIS", Params);

                    if (result != null)
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

        public DataSet getDataConsol(ConsoleDto obj)
        {
            var Params = ParamConsol(obj, 1);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisConsolReport", Params);

                    if (result != null)
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