using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class BackMenuData
    {
        SqlParameter[] Param(BackMenuDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@BackMenuId", SqlDbType.Int);
            Param[0].Value = obj.BackMenuId;

            Param[1] = new SqlParameter("@BackMenuName", SqlDbType.NVarChar);
            Param[1].Value = obj.BackMenuName;

            Param[2] = new SqlParameter("@BackMenuParentId", SqlDbType.Int);
            Param[2].Value = obj.BackMenuParentId;

            Param[3] = new SqlParameter("@BackMenuURL", SqlDbType.NVarChar);
            Param[3].Value = obj.BackMenuURL;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;

            return Param;
        }
        public bool Insert(BackMenuDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspBackMenu", Params);

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
        public bool Update(BackMenuDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspBackMenu", Params);

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
        public BackMenuDto GetById(int id)
        {
            DataSet _ds = null;

            BackMenuDto _BackMenu = new BackMenuDto();
            _BackMenu.BackMenuId = id;
            var Params = Param(_BackMenu, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBackMenu", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        BackMenuDto result = new BackMenuDto();
                        {
                            result.BackMenuId = row["BackMenuId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BackMenuId"]);
                            result.BackMenuName = row["BackMenuName"].ToString();
                            result.BackMenuParentId = row["BackMenuParentId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["BackMenuParentId"]);
                        }
                        return result;
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
            return null;
        }
        public IList<BackMenuDto> GetAll()
        {
            DataSet _ds = null;
            var Params = Param(new BackMenuDto(), 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBackMenu", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<BackMenuDto> results = new List<BackMenuDto>();

                        foreach (DataRow row in rows)
                        {
                            BackMenuDto obj = new BackMenuDto();

                            obj.BackMenuId = row["BackMenuId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BackMenuId"]);
                            obj.BackMenuName = row["BackMenuName"].ToString();
                            obj.BackMenuParentId = row["BackMenuParentId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["BackMenuParentId"]);
                            obj.BackMenuURL = row["BackMenuURL"].ToString();
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
        public IList<BackMenuDto> GetAllParent()
        {
            DataSet _ds = null;
            var Params = Param(new BackMenuDto(), 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBackMenu", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<BackMenuDto> results = new List<BackMenuDto>();

                        foreach (DataRow row in rows)
                        {
                            BackMenuDto obj = new BackMenuDto();

                            obj.BackMenuId = row["BackMenuId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BackMenuId"]);
                            obj.BackMenuName = row["BackMenuName"].ToString();
                            obj.BackMenuParentId = row["BackMenuParentId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["BackMenuParentId"]);
                            obj.BackMenuURL = row["BackMenuURL"].ToString();
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
        public IList<BackMenuDto> GetAllMenuAccordingPermission()
        {
             
            DataSet _ds = null;
            BackMenuDto _backmeu = new BackMenuDto();
            _backmeu.BackMenuId = LovelySession.Lovely.User.UserTypeId.Value;
            _backmeu.BackMenuParentId = LovelySession.Lovely.User.Id.Value.ToDataConvertInt32();
            var Params = Param(_backmeu, 6);
            if (Params != null)
            {
                try 
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBackMenu", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<BackMenuDto> results = new List<BackMenuDto>();

                        foreach (DataRow row in rows)
                        {
                            BackMenuDto obj = new BackMenuDto();

                            obj.BackMenuId = row["BackMenuId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BackMenuId"]);
                            obj.BackMenuName = row["BackMenuName"].ToString();
                            obj.BackMenuParentId = row["BackMenuParentId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["BackMenuParentId"]);
                            obj.BackMenuURL = row["BackMenuURL"].ToString();
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
