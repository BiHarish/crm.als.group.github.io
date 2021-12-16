using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class ContainerMasterData
    {
        SqlParameter[] Param(ContainerMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[4];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@ContCode", SqlDbType.NVarChar);
            Param[1].Value = obj.ContCode;

            Param[2] = new SqlParameter("@ContDescription", SqlDbType.NVarChar);
            Param[2].Value = obj.ContDescription;

            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            return Param;
        }
        public bool Insert(ContainerMasterDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspContainerMaster", Params);

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
        public bool Update(ContainerMasterDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspContainerMaster", Params);

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
        public ContainerMasterDto GetById(long id)
        {
            DataSet _ds = null;

            ContainerMasterDto _containerMasterData = new ContainerMasterDto();
            _containerMasterData.ID = id;
            var Params = Param(_containerMasterData, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspContainerMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        ContainerMasterDto result = new ContainerMasterDto();
                        {
                            result.ID = row["ID"].ToDataConvertInt64();
                            result.ContCode = row["ContCode"].ToString();
                            result.ContDescription = row["ContDescription"].ToString();
                        }
                        return result;
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
        public IList<ContainerMasterDto> GetAll(bool Type)
        {
            DataSet _ds = null;
            ContainerMasterDto _containerMasterData = new ContainerMasterDto();
           
            var Params = Param(_containerMasterData, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspContainerMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<ContainerMasterDto> results = new List<ContainerMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            ContainerMasterDto obj = new ContainerMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.ContCode = row["ContCode"].ToString();
                            obj.ContDescription = row["ContDescription"].ToString();

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