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
    public class MISData
    {
        SqlParameter[] Param(MISDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[47];
            Param[0] = new SqlParameter("@maid", SqlDbType.BigInt);
            Param[0].Value = obj.maid;
            Param[1] = new SqlParameter("@misdivisionid", SqlDbType.BigInt);
            Param[1].Value = obj.misdivisionid;
            Param[2] = new SqlParameter("@mistyid", SqlDbType.BigInt);
            Param[2].Value = obj.mistyid;
            Param[3] = new SqlParameter("@mafinancialyear", SqlDbType.NVarChar);
            Param[3].Value = obj.mafinancialyear;
            Param[4] = new SqlParameter("@mampmid", SqlDbType.BigInt);
            Param[4].Value = obj.mampmid;
            Param[5] = new SqlParameter("@maApr", SqlDbType.Float);
            Param[5].Value = obj.maApr;
            Param[6] = new SqlParameter("@maMay", SqlDbType.Float);
            Param[6].Value = obj.maMay;
            Param[7] = new SqlParameter("@maJun", SqlDbType.Float);
            Param[7].Value = obj.maJun;
            Param[8] = new SqlParameter("@maJul", SqlDbType.Float);
            Param[8].Value = obj.maJul;
            Param[9] = new SqlParameter("@maAug", SqlDbType.Float);
            Param[9].Value = obj.maAug;
            Param[10] = new SqlParameter("@maSep", SqlDbType.Float);
            Param[10].Value = obj.maSep;
            Param[11] = new SqlParameter("@maOct", SqlDbType.Float);
            Param[11].Value = obj.maOct;
            Param[12] = new SqlParameter("@maNov", SqlDbType.Float);
            Param[12].Value = obj.maNov;
            Param[13] = new SqlParameter("@maDec", SqlDbType.Float);
            Param[13].Value = obj.maDec;
            Param[14] = new SqlParameter("@maJan", SqlDbType.Float);
            Param[14].Value = obj.maJan;
            Param[15] = new SqlParameter("@maFeb", SqlDbType.Float);
            Param[15].Value = obj.maFeb;
            Param[16] = new SqlParameter("@maMar", SqlDbType.Float);
            Param[16].Value = obj.maMar;
            Param[17] = new SqlParameter("@maTotal", SqlDbType.Float);
            Param[17].Value = obj.maTotal;
            Param[18] = new SqlParameter("@maYtd", SqlDbType.Float);
            Param[18].Value = obj.maYtd;
            Param[19] = new SqlParameter("@createby", SqlDbType.BigInt);
            Param[19].Value = obj.createby;
            Param[20] = new SqlParameter("@maActualRR", SqlDbType.Float);
            Param[20].Value = obj.maActualRR;
            Param[21] = new SqlParameter("@maBaltoAchieve", SqlDbType.Float);
            Param[21].Value = obj.maBaltoAchieve;
            Param[22] = new SqlParameter("@maRequiredRR", SqlDbType.Float);
            Param[22].Value = obj.maRequiredRR;
            Param[23] = new SqlParameter("@marid", SqlDbType.BigInt);
            Param[23].Value = obj.marid;
            Param[24] = new SqlParameter("@marMonth", SqlDbType.Float);
            Param[24].Value = obj.marMonth;
            Param[25] = new SqlParameter("@marMonthName", SqlDbType.NVarChar);
            Param[25].Value = obj.marMonthName;
            Param[26] = new SqlParameter("@marRemarks", SqlDbType.NVarChar);
            Param[26].Value = obj.marRemarks;
            Param[27] = new SqlParameter("@marvTotal", SqlDbType.Float);
            Param[27].Value = obj.marvTotal;
            Param[28] = new SqlParameter("@marRemarkYtd", SqlDbType.BigInt);
            Param[28].Value = obj.marRemarkYtd;
            Param[29] = new SqlParameter("@division", SqlDbType.NVarChar);
            Param[29].Value = obj.division;
            Param[30] = new SqlParameter("@mistype", SqlDbType.NVarChar);
            Param[30].Value = obj.mistype;
            Param[31] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[31].Value = flag;
            Param[32] = new SqlParameter("@maAprWS", SqlDbType.Float);
            Param[32].Value = obj.maAprWS;
            Param[33] = new SqlParameter("@maMayWS", SqlDbType.Float);
            Param[33].Value = obj.maMayWS;
            Param[34] = new SqlParameter("@maJunWS", SqlDbType.Float);
            Param[34].Value = obj.maJunWS;
            Param[35] = new SqlParameter("@maJulWS", SqlDbType.Float);
            Param[35].Value = obj.maJulWS;
            Param[36] = new SqlParameter("@maAugWS", SqlDbType.Float);
            Param[36].Value = obj.maAugWS;
            Param[37] = new SqlParameter("@maSepWS", SqlDbType.Float);
            Param[37].Value = obj.maSepWS;
            Param[38] = new SqlParameter("@maOctWS", SqlDbType.Float);
            Param[38].Value = obj.maOctWS;
            Param[39] = new SqlParameter("@maNovWS", SqlDbType.Float);
            Param[39].Value = obj.maNovWS;
            Param[40] = new SqlParameter("@maDecWS", SqlDbType.Float);
            Param[40].Value = obj.maDecWS;
            Param[41] = new SqlParameter("@maJanWS", SqlDbType.Float);
            Param[41].Value = obj.maJanWS;
            Param[42] = new SqlParameter("@maFebWS", SqlDbType.Float);
            Param[42].Value = obj.maFebWS;
            Param[43] = new SqlParameter("@maMarWS", SqlDbType.Float);
            Param[43].Value = obj.maMarWS;
            Param[44] = new SqlParameter("@maTotalWS", SqlDbType.Float);
            Param[44].Value = obj.maTotalWS;
            Param[45] = new SqlParameter("@SEISType", SqlDbType.NVarChar);
            Param[45].Value = obj.SEISType;
            Param[46] = new SqlParameter("@uniqueID", SqlDbType.NVarChar);
            Param[46].Value = obj.uniqueID;

            return Param;
        }

        public string Savemisdata(MISDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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
        public DataSet GetMisdata(MISDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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
        public DataSet GetMisdatabyFYearandDivision(MISDto obj)
        {
            var Params = Param(obj, 8);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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

        public DataSet BindDivision(MISDto obj)
        {
            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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

        public DataSet BindMIStype(MISDto obj)
        {
            var Params = Param(obj, 4);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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
        public DataSet BindSubDivision(MISDto obj)
        {
            var Params = Param(obj, 6);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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
        public DataSet getDataFromParticularMaster(MISDto mis)
        {
            var Params = Param(mis, 5);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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

        public DataSet GetAll(string year, int? divisionid)
        {
            MISDto obj = new MISDto();
            obj.mafinancialyear = year;
            obj.misdivisionid = divisionid;
            var Params = Param(obj, 9);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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

        public DataSet MisFinDashboard(MISDto obj)
        {
            var Params = Param(obj, 10);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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

        public DataSet getForcastRevenue(MISDto obj)
        {
            var Params = Param(obj, 11);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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

        public DataSet getRevenueVsCost(MISDto obj)
        {
            var Params = Param(obj, 12);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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

        public DataSet getRevenueMOM(MISDto obj)
        {
            var Params = Param(obj, 13);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspmisactuals", Params);

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