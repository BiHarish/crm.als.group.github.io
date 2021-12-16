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
    public class MisBudgetData
    {
        SqlParameter[] Param(MisBudgetDto obj, int flag)
        { 

            SqlParameter[] Param = new SqlParameter[22];
            Param[0] = new SqlParameter("@mbid", SqlDbType.BigInt);
            Param[0].Value = obj.mbid;
            Param[1] = new SqlParameter("@mbdivisionid", SqlDbType.BigInt);
            Param[1].Value = obj.mbdivisionid;
            Param[2] = new SqlParameter("@mbmtyid", SqlDbType.BigInt);
            Param[2].Value = obj.mbmtyid;
            Param[3] = new SqlParameter("@mbfinancialyear", SqlDbType.NVarChar);
            Param[3].Value = obj.mbfinancialyear;
            Param[4] = new SqlParameter("@mbmpmid", SqlDbType.BigInt);
            Param[4].Value = obj.mbmpmid;
            Param[5] = new SqlParameter("@mbApr", SqlDbType.Float);
            Param[5].Value = obj.mbApr;
            Param[6] = new SqlParameter("@mbMay", SqlDbType.Float);
            Param[6].Value = obj.mbMay;
            Param[7] = new SqlParameter("@mbJun", SqlDbType.Float);
            Param[7].Value = obj.mbJun;
            Param[8] = new SqlParameter("@mbJul", SqlDbType.Float);
            Param[8].Value = obj.mbJul;
            Param[9] = new SqlParameter("@mbAug", SqlDbType.Float);
            Param[9].Value = obj.mbAug;
            Param[10] = new SqlParameter("@mbSep", SqlDbType.Float);
            Param[10].Value = obj.mbSep;
            Param[11] = new SqlParameter("@mbOct", SqlDbType.Float);
            Param[11].Value = obj.mbOct;
            Param[12] = new SqlParameter("@mbNov", SqlDbType.Float);
            Param[12].Value = obj.mbNov;
            Param[13] = new SqlParameter("@mbDec", SqlDbType.Float);
            Param[13].Value = obj.mbDec;
            Param[14] = new SqlParameter("@mbJan", SqlDbType.Float);
            Param[14].Value = obj.mbJan;
            Param[15] = new SqlParameter("@mbFeb", SqlDbType.Float);
            Param[15].Value = obj.mbFeb;
            Param[16] = new SqlParameter("@mbMar", SqlDbType.Float);
            Param[16].Value = obj.mbMar;
            Param[17] = new SqlParameter("@mbTotal", SqlDbType.Float);
            Param[17].Value = obj.mbTotal;
            Param[18] = new SqlParameter("@mbYtd", SqlDbType.Float);
            Param[18].Value = obj.mbYtd;
            Param[19] = new SqlParameter("@mbcreateby", SqlDbType.BigInt);
            Param[19].Value = obj.mbcreateby;
            Param[20] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[20].Value = flag;
            Param[21] = new SqlParameter("@MonthName", SqlDbType.NVarChar);
            Param[21].Value = obj.MonthName;

            return Param;
        }

        public long Insert(MisBudgetDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspMisBudget", Params);

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

        public bool Update(MisBudgetDto obj)
        {
            var Params = Param(obj, 4);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspMisBudget", Params);

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

        public DataSet getDataFromBudgetTable(MisBudgetDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisBudget", Params);

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

        public DataSet getDataFromParticularMaster(MisBudgetDto obj)
        {
            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisBudget", Params);

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

        public DataSet getDataFromBudgetTableForList(MisBudgetDto obj)
        {
            var Params = Param(obj, 6);
            if (Params != null) 
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisBudget", Params);

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

        public DataSet GetAll(string year, long? divisionid)
        {
            MisBudgetDto obj = new MisBudgetDto();
            obj.mbfinancialyear = year;
            obj.mbdivisionid = divisionid;

            var Params = Param(obj, 5);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisBudget", Params);

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




        //Budget VS Actual

        public DataSet getBudgetVsActualReport1(MisBudgetDto obj)
        {
            var Params = Param(obj, 7);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisBudget", Params);

                    if (result != null && result.Tables!=null && result.Tables[0].Rows.Count > 0)
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

        public DataSet getBudgetVsActualReport2(MisBudgetDto obj)
        {
            var Params = Param(obj, 8);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisBudget", Params);

                    if (result != null && result.Tables != null && result.Tables[0].Rows.Count > 0)
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


        public DataSet getBudgetVsActualReport3(MisBudgetDto obj)
        {
            var Params = Param(obj, 9);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisBudget", Params);

                    if (result != null && result.Tables != null && result.Tables[0].Rows.Count > 0)
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

        public long uploadBudgetOrActual(UploadBudgetAndActualDto obj)
        {
            SqlParameter[] Param = new SqlParameter[4];

            Param[0] = new SqlParameter("@Upload", obj.data);
            Param[1] = new SqlParameter("@FinYear", obj.FinYear);
            Param[2] = new SqlParameter("@TypeID", obj.TypeID);
            Param[3] = new SqlParameter("@DivID", obj.DivID);

            if (Param != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspBudgetAndActualUpload", Param);

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

        public DataSet BudgetVsActualConsol(MisBudgetVsActualConsolDto obj)
        {
            SqlParameter[] P = new SqlParameter[4];
            P[0] = new SqlParameter("@financialyear", SqlDbType.NVarChar);
            P[0].Value = obj.financialyear;
            P[1] = new SqlParameter("@division", SqlDbType.NVarChar);
            P[1].Value = obj.division;
            P[2] = new SqlParameter("@month", SqlDbType.BigInt);
            P[2].Value = obj.month;
            P[3] = new SqlParameter("@Flag", SqlDbType.BigInt);
            P[3].Value = 1;

            
            if (P != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisActualvsBudgetConsol", P);

                    if (result != null && result.Tables != null && result.Tables[0].Rows.Count > 0)
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