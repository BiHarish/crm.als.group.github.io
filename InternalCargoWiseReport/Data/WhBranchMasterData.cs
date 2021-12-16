using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class WhBranchMasterData
    {
        SqlParameter[] Param(WhBranchMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];

            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = obj.Name;

            Param[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
            Param[2].Value = obj.IsActive;

            Param[3] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[3].Value = obj.CreateBy;

            Param[4] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[4].Value = flag;
           

            return Param;
        }

       
        public IList<WhBranchMasterDto> GetAll()
        {
            DataSet _ds = null;
            WhBranchMasterDto _WhBranchMasterData = new WhBranchMasterDto();

            var Params = Param(_WhBranchMasterData,1);

            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhBranchMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhBranchMasterDto> results = new List<WhBranchMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WhBranchMasterDto obj = new WhBranchMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Name = row["Name"].ToString();

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