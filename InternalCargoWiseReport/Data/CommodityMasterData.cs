using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class CommodityMasterData
    {
        SqlParameter[] Param(CommodityMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[8];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = obj.Name;

            Param[2] = new SqlParameter("@Code", SqlDbType.NVarChar);
            Param[2].Value = obj.Code;

            Param[3] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[3].Value = obj.CreateBy;

            Param[4] = new SqlParameter("@CreateOn", SqlDbType.DateTime);
            Param[4].Value = obj.CreateOn;

            Param[5] = new SqlParameter("@ModifyBy", SqlDbType.BigInt);
            Param[5].Value = obj.ModifyBy;

            Param[6] = new SqlParameter("@ModifyOn", SqlDbType.DateTime);
            Param[6].Value = obj.ModifyOn;

            Param[7] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[7].Value = flag;

            return Param;
        }
        
        public IList<CommodityMasterDto> GetAll()
        {
            DataSet _ds = null;
            CommodityMasterDto _commodityMaster = new CommodityMasterDto();
            var Params = Param(_commodityMaster, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCommodityMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CommodityMasterDto> results = new List<CommodityMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            CommodityMasterDto obj = new CommodityMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Name = row["Name"].ToString();
                            obj.Code = row["Code"].ToString();

                            results.Add(obj);
                        }
                        return results;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
    
    }
}