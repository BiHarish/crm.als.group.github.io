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
    public class MisVolumeActualAndBudgetData
    {
        SqlParameter[] Param(MisVolumeActualAndBudgetDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[27];
            Param[0] = new SqlParameter("@mvaid", SqlDbType.BigInt);
            Param[0].Value = obj.mvaid;
            Param[1] = new SqlParameter("@mvadivisionid", SqlDbType.BigInt);
            Param[1].Value = obj.mvadivisionid;
            Param[2] = new SqlParameter("@mvavtyid", SqlDbType.BigInt);
            Param[2].Value = obj.mvavtyid;
            Param[3] = new SqlParameter("@mvafinancialyear", SqlDbType.NVarChar);
            Param[3].Value = obj.mvafinancialyear;
            Param[4] = new SqlParameter("@mvampmid", SqlDbType.BigInt);
            Param[4].Value = obj.mvampmid;
            Param[5] = new SqlParameter("@mvaApr", SqlDbType.Float);
            Param[5].Value = obj.mvaApr;
            Param[6] = new SqlParameter("@mvaMay", SqlDbType.Float);
            Param[6].Value = obj.mvaMay;
            Param[7] = new SqlParameter("@mvaJun", SqlDbType.Float);
            Param[7].Value = obj.mvaJun;
            Param[8] = new SqlParameter("@mvaJul", SqlDbType.Float);
            Param[8].Value = obj.mvaJul;
            Param[9] = new SqlParameter("@mvaAug", SqlDbType.Float);
            Param[9].Value = obj.mvaAug;
            Param[10] = new SqlParameter("@mvaSep", SqlDbType.Float);
            Param[10].Value = obj.mvaSep;
            Param[11] = new SqlParameter("@mvaOct", SqlDbType.Float);
            Param[11].Value = obj.mvaOct;
            Param[12] = new SqlParameter("@mvaNov", SqlDbType.Float);
            Param[12].Value = obj.mvaNov;
            Param[13] = new SqlParameter("@mvaDec", SqlDbType.Float);
            Param[13].Value = obj.mvaDec;
            Param[14] = new SqlParameter("@mvaJan", SqlDbType.Float);
            Param[14].Value = obj.mvaJan;
            Param[15] = new SqlParameter("@mvaFeb", SqlDbType.Float);
            Param[15].Value = obj.mvaFeb;
            Param[16] = new SqlParameter("@mvaMar", SqlDbType.Float);
            Param[16].Value = obj.mvaMar;
            Param[17] = new SqlParameter("@mvaTotal", SqlDbType.Float);
            Param[17].Value = obj.mvaTotal;
            Param[18] = new SqlParameter("@mvaYtd", SqlDbType.Float);
            Param[18].Value = obj.mvaYtd;
            Param[19] = new SqlParameter("@mvacreateby", SqlDbType.BigInt);
            Param[19].Value = obj.mvacreateby;
            Param[20] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[20].Value = flag;
            Param[21] = new SqlParameter("@mvamptid", SqlDbType.BigInt);
            Param[21].Value = obj.mvamptid;
            Param[22] = new SqlParameter("@mvaActualRR", SqlDbType.Float);
            Param[22].Value = obj.mvaActualRR;
            Param[23] = new SqlParameter("@mvaBaltoAchieve", SqlDbType.Float);
            Param[23].Value = obj.mvaBaltoAchieve;
            Param[24] = new SqlParameter("@mvaRequiredRR", SqlDbType.Float);
            Param[24].Value = obj.mvaRequiredRR;
            Param[25] = new SqlParameter("@PeriodTypeID", SqlDbType.BigInt);
            Param[25].Value = obj.PeriodTypeID;
            Param[26] = new SqlParameter("@TypeDesc", SqlDbType.NVarChar);
            Param[26].Value = obj.TypeDesc;

            return Param;
        }

        public long Insert(MisVolumeActualAndBudgetDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspMisVolumeActualAndBudget", Params);

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


    }
}