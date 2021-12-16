using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ICWR.Data
{
    public class BDSolutionMasterData 
    {
        SqlParameter[] Param(BDSolutionMasterDto obj, int flag)
        { 

            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@BD", SqlDbType.NVarChar);
            Param[1].Value = obj.BD;

            Param[2] = new SqlParameter("@Solution", SqlDbType.NVarChar);
            Param[2].Value = obj.Solution;

            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            Param[4] = new SqlParameter("@FFBD", SqlDbType.NVarChar);
            Param[4].Value = obj.FFBD;


            return Param;
        }

        public long Insert(BDSolutionMasterDto obj)
        {
            var Params = Param(obj, 8);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspBDSolutionMaster", Params);

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

        public bool Update(BDSolutionMasterDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspBDSolutionMaster", Params);

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

        public BDSolutionMasterDto GetById(long id)
        {
            DataSet _ds = null;

            BDSolutionMasterDto LeadData = new BDSolutionMasterDto();
            LeadData.ID = id;
            var Params = Param(LeadData, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBDSolutionMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        BDSolutionMasterDto result = new BDSolutionMasterDto();
                        {
                            result.ID = row["ID"].ToDataConvertInt64();
                            result.BD = row["BD"].ToString();
                            result.Solution = row["Solution"].ToString();
                            result.FFBD = row["FFBD"].ToString();
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

        public IList<BDSolutionMasterDto> GetAllBD()
        {
            DataSet _ds = null;
            BDSolutionMasterDto _whLeadMaster = new BDSolutionMasterDto();
           
            var Params = Param(_whLeadMaster, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBDSolutionMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<BDSolutionMasterDto> results = new List<BDSolutionMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            BDSolutionMasterDto obj = new BDSolutionMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.BD = row["Name"].ToString();
                            obj.Solution = row["Name"].ToString();

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

        public IList<BDSolutionMasterDto> GetAllSolution()
        {
            DataSet _ds = null;
            BDSolutionMasterDto _whLeadMaster = new BDSolutionMasterDto();

            var Params = Param(_whLeadMaster, 3); //old 4
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBDSolutionMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<BDSolutionMasterDto> results = new List<BDSolutionMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            BDSolutionMasterDto obj = new BDSolutionMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.BD = row["Name"].ToString();
                            obj.Solution = row["Name"].ToString();

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

        public IList<BDSolutionMasterDto> GetAllFFBD()
        {
            DataSet _ds = null;
            BDSolutionMasterDto _whLeadMaster = new BDSolutionMasterDto();

            var Params = Param(_whLeadMaster, 3); //old 5
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBDSolutionMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<BDSolutionMasterDto> results = new List<BDSolutionMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            BDSolutionMasterDto obj = new BDSolutionMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.FFBD = row["Name"].ToString();

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

        public IList<BDSolutionMasterDto> GetAllCFSBD()
        {
            DataSet _ds = null;
            BDSolutionMasterDto _whLeadMaster = new BDSolutionMasterDto();

            var Params = Param(_whLeadMaster, 3); //old 6
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBDSolutionMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<BDSolutionMasterDto> results = new List<BDSolutionMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            BDSolutionMasterDto obj = new BDSolutionMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.CFSBD = row["Name"].ToString();

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

        public IList<RouteMasterDto> getAllRoute()
        {
            DataSet _ds = null;
            BDSolutionMasterDto _whLeadMaster = new BDSolutionMasterDto();

            var Params = Param(_whLeadMaster, 7);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBDSolutionMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<RouteMasterDto> results = new List<RouteMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            RouteMasterDto obj = new RouteMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Route = row["Route"].ToString();

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

        public DataSet GetPendingApprovalList(long? ID,string BD)
        {
            DataSet _ds = null;
            BDSolutionMasterDto _whLeadMaster = new BDSolutionMasterDto();
            _whLeadMaster.ID = ID.ToDataConvertInt64();
            _whLeadMaster.BD = BD;

            var Params = Param(_whLeadMaster, 9); 
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBDSolutionMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        
                        return _ds;
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