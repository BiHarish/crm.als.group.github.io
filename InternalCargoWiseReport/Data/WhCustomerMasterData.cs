using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class WhCustomerMasterData
    {
        SqlParameter[] Param(WhCustomerMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[15];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = obj.Name;

            Param[2] = new SqlParameter("@Address", SqlDbType.NVarChar);
            Param[2].Value = obj.Address;

            Param[3] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[3].Value = obj.CreateBy;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;

            Param[5] = new SqlParameter("@PhoneNo", SqlDbType.BigInt);
            Param[5].Value = obj.PhoneNo;
            Param[6] = new SqlParameter("@GSTNo", SqlDbType.NVarChar);
            Param[6].Value = obj.GSTNo;
            Param[7] = new SqlParameter("@EmailID", SqlDbType.NVarChar);
            Param[7].Value = obj.EmailID;
            Param[8] = new SqlParameter("@PinCode", SqlDbType.BigInt);
            Param[8].Value = obj.PinCode;
            Param[9] = new SqlParameter("@SCS", SqlDbType.Bit);
            Param[9].Value = obj.SCS;
            Param[10] = new SqlParameter("@FF", SqlDbType.Bit);
            Param[10].Value = obj.FF;
            Param[11] = new SqlParameter("@Prime", SqlDbType.Bit);
            Param[11].Value = obj.Prime;
            Param[12] = new SqlParameter("@Liquid", SqlDbType.Bit);
            Param[12].Value = obj.Liquid;
            Param[13] = new SqlParameter("@CFS", SqlDbType.Bit);
            Param[13].Value = obj.CFS;
            Param[14] = new SqlParameter("@BusinessUnit", SqlDbType.NVarChar);
            Param[14].Value = obj.BusinessUnit;

            return Param;
        }
        SqlParameter[] ParamTrans(WhCustomerAddTransDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@CustAddress", SqlDbType.NVarChar);
            Param[1].Value = obj.CustAddress;

            Param[2] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[2].Value = obj.CreateBy;

            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            Param[4] = new SqlParameter("@WhCustomerID", SqlDbType.BigInt);
            Param[4].Value = obj.WhCustomerID;

          
            return Param;
        }
        public long Insert(WhCustomerMasterDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCustomerMaster", Params);

                    if (i != null)
                    {
                        return i.ToDataConvertInt64();
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {

                }
            }
            return 0;
        }
        public bool Update(WhCustomerMasterDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCustomerMaster", Params);

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
        public WhCustomerMasterDto GetById(long? id)
        {
            DataSet _ds = null;

            WhCustomerMasterDto _WhCustomerMaster = new WhCustomerMasterDto();
            _WhCustomerMaster.ID = id;
            var Params = Param(_WhCustomerMaster, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCustomerMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhCustomerMasterDto result = new WhCustomerMasterDto();
                        {
                            result.ID = row["ID"].ToDataConvertNullInt64();
                            result.Name = row["Name"].ToString();
                            result.Address = row["Address"].ToString();
                            result.CreateBy = row["CreateBy"].ToDataConvertNullInt64();
                            result.PhoneNo = row["PhoneNo"].ToDataConvertNullInt64();
                            result.PinCode = row["PinCode"].ToDataConvertNullInt64();
                            result.GSTNo = row["GSTNo"].ToString();
                            result.EmailID = row["EmailID"].ToString();
                            result.BusinessUnit = row["BusinessUnit"].ToString();
                            result.SCS = row["SCS"].ToDataConvertBool();
                            result.FF = row["FF"].ToDataConvertBool();
                            result.Prime = row["Prime"].ToDataConvertBool();
                            result.Liquid = row["Liquid"].ToDataConvertBool();
                            result.CFS = row["CFS"].ToDataConvertBool();
                            
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
        public IList<WhCustomerMasterDto> GetAll(string BU)
        { 
            DataSet _ds = null;
            WhCustomerMasterDto _WhCustomerMaster = new WhCustomerMasterDto();
            _WhCustomerMaster.BusinessUnit = BU;
            var Params = Param(_WhCustomerMaster, 5);//OLD FLAG 4
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCustomerMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhCustomerMasterDto> results = new List<WhCustomerMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WhCustomerMasterDto obj = new WhCustomerMasterDto();

                            obj.ID = row["ID"].ToDataConvertNullInt64();
                            obj.Name = row["Name"].ToString();
                            obj.Address = row["Address"].ToString();
                            obj.CreateBy = row["CreateBy"].ToDataConvertNullInt64();
                            obj.PhoneNo = row["PhoneNo"].ToDataConvertNullInt64(); 
                            obj.PinCode = row["PinCode"].ToDataConvertNullInt64();
                            obj.GSTNo = row["GSTNo"].ToString();
                            obj.EmailID = row["EmailID"].ToString();
                            obj.BusinessUnit = row["BusinessUnit"].ToString();
                            obj.SCS = row["SCS"].ToDataConvertBool();
                            obj.FF = row["FF"].ToDataConvertBool();
                            obj.Prime = row["Prime"].ToDataConvertBool();
                            obj.Liquid = row["Liquid"].ToDataConvertBool();
                            obj.CFS = row["CFS"].ToDataConvertBool();

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
        public WhCustomerMasterDto checkCustomerBlongTo(long? id,string BusinessUnit)
        {
            DataSet _ds = null; 

            WhCustomerMasterDto _WhCustomerMaster = new WhCustomerMasterDto();
            _WhCustomerMaster.ID = id;
            _WhCustomerMaster.BusinessUnit = BusinessUnit;
            var Params = Param(_WhCustomerMaster, 6);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCustomerMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhCustomerMasterDto result = new WhCustomerMasterDto();
                        {
                            result.BelongTo = row["BelongTo"].ToString();
                            result.Result = row["Result"].ToString();

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



        //Customer Address Transaction

        public bool InsertAddress(WhCustomerAddTransDto obj)
        {
            var Params = ParamTrans(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "USPWhCustomerAddTrans", Params);

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
        public bool UpdateAddress(WhCustomerAddTransDto obj)
        {
            var Params = ParamTrans(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "USPWhCustomerAddTrans", Params);

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
        public WhCustomerAddTransDto GetByIdAddress(long? id)
        {
            DataSet _ds = null;

            WhCustomerAddTransDto _WhCustomerMaster = new WhCustomerAddTransDto();
            _WhCustomerMaster.ID = id;
            var Params = ParamTrans(_WhCustomerMaster, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "USPWhCustomerAddTrans", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhCustomerAddTransDto result = new WhCustomerAddTransDto();
                        {
                            result.ID = row["ID"].ToDataConvertNullInt64();
                            result.CustAddress = row["CustAddress"].ToString();
                            result.CreateBy = row["CreateBy"].ToDataConvertNullInt64();
                           

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
        public IList<WhCustomerAddTransDto> GetAllAddressByCustomerID(long? ID)
        {
            DataSet _ds = null;
            WhCustomerAddTransDto _WhCustomerMaster = new WhCustomerAddTransDto();
            _WhCustomerMaster.WhCustomerID = ID;
          
            var Params = ParamTrans(_WhCustomerMaster, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "USPWhCustomerAddTrans", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhCustomerAddTransDto> results = new List<WhCustomerAddTransDto>();

                        foreach (DataRow row in rows)
                        {
                            WhCustomerAddTransDto obj = new WhCustomerAddTransDto();

                            obj.ID = row["ID"].ToDataConvertNullInt64();
                            obj.WhCustomerID = row["WhCustomerID"].ToDataConvertNullInt64();
                            obj.CustAddress = row["CustAddress"].ToString();
                            obj.CreateBy = row["CreateBy"].ToDataConvertNullInt64();
                            obj.RowNumber = row["RowNumber"].ToString();
                           

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

        public bool deleteAddress(long id)
        {
            WhCustomerAddTransDto obj = new WhCustomerAddTransDto();
            obj.ID = id;
            var Params = ParamTrans(obj, 5);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "USPWhCustomerAddTrans", Params);

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

        public IList<WhCustomerMasterDto> GetAllByBU(string BU)
        {
            DataSet _ds = null;
            WhCustomerMasterDto _WhCustomerMaster = new WhCustomerMasterDto();
            _WhCustomerMaster.BusinessUnit = BU;
            var Params = Param(_WhCustomerMaster, 4);//OLD FLAG 4
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCustomerMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhCustomerMasterDto> results = new List<WhCustomerMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WhCustomerMasterDto obj = new WhCustomerMasterDto();

                            obj.ID = row["ID"].ToDataConvertNullInt64();
                            obj.Name = row["Name"].ToString();
                            obj.Address = row["Address"].ToString();
                            obj.CreateBy = row["CreateBy"].ToDataConvertNullInt64();
                            obj.PhoneNo = row["PhoneNo"].ToDataConvertNullInt64();
                            obj.PinCode = row["PinCode"].ToDataConvertNullInt64();
                            obj.GSTNo = row["GSTNo"].ToString();
                            obj.EmailID = row["EmailID"].ToString();
                            obj.BusinessUnit = row["BusinessUnit"].ToString();
                            obj.SCS = row["SCS"].ToDataConvertBool();
                            obj.FF = row["FF"].ToDataConvertBool();
                            obj.Prime = row["Prime"].ToDataConvertBool();
                            obj.Liquid = row["Liquid"].ToDataConvertBool();
                            obj.CFS = row["CFS"].ToDataConvertBool();

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