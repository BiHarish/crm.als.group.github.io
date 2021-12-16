using ICWR.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;

namespace ICWR.Data
{
    public class CompanyData
    {
        SqlParameter[] Param(CompanyDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[21];
            Param[0] = new SqlParameter("@CompanyId", SqlDbType.Int);
            Param[0].Value = obj.CompanyId;

            Param[1] = new SqlParameter("@CompanyName", SqlDbType.NVarChar);
            Param[1].Value = obj.CompanyName;

            Param[2] = new SqlParameter("@CompanyCode", SqlDbType.VarChar);
            Param[2].Value = obj.CompanyCode;

            Param[3] = new SqlParameter("@CompanyGSTNo", SqlDbType.NVarChar);
            Param[3].Value = obj.CompanyGSTNo;

            Param[4] = new SqlParameter("@CompanyWebsite", SqlDbType.NVarChar);
            Param[4].Value = obj.CompanyWebsite;

            Param[5] = new SqlParameter("@CompanyEmailId", SqlDbType.VarChar);
            Param[5].Value = obj.CompanyEmailId;

            Param[6] = new SqlParameter("@CompanyMobileNo", SqlDbType.VarChar);
            Param[6].Value = obj.CompanyMobileNo;

            Param[7] = new SqlParameter("@CompanyFax", SqlDbType.NVarChar);
            Param[7].Value = obj.CompanyFax;

            Param[8] = new SqlParameter("@CompanyPhone1", SqlDbType.VarChar);
            Param[8].Value = obj.CompanyPhone1;

            Param[9] = new SqlParameter("@CompanyPhone2", SqlDbType.VarChar);
            Param[9].Value = obj.CompanyPhone2;

            Param[10] = new SqlParameter("@CompanyStateId", SqlDbType.BigInt);
            Param[10].Value = obj.CompanyStateId;

            Param[11] = new SqlParameter("@CompanyCityId", SqlDbType.BigInt);
            Param[11].Value = obj.CompanyCityId;

            Param[12] = new SqlParameter("@CompanyAddress", SqlDbType.NVarChar);
            Param[12].Value = obj.CompanyAddress;

            Param[13] = new SqlParameter("@CompanyZipCode", SqlDbType.VarChar);
            Param[13].Value = obj.CompanyZipCode;

            Param[14] = new SqlParameter("@CompanyBankId", SqlDbType.Int);
            Param[14].Value = obj.CompanyBankId;

            Param[15] = new SqlParameter("@CompanyAccountNo", SqlDbType.VarChar);
            Param[15].Value = obj.CompanyAccountNo;

            Param[16] = new SqlParameter("@CompanyIFSCode", SqlDbType.VarChar);
            Param[16].Value = obj.CompanyIFSCode;

            Param[17] = new SqlParameter("@CompanyBankBranch", SqlDbType.NVarChar);
            Param[17].Value = obj.CompanyBankBranch;

            Param[18] = new SqlParameter("@CompanyLogo", SqlDbType.VarChar);
            Param[18].Value = obj.CompanyLogo;

            Param[19] = new SqlParameter("@CompanyIsActive", SqlDbType.Bit);
            Param[19].Value = obj.CompanyIsActive;

            Param[20] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[20].Value = flag;

            return Param;
        }
        public CompanyDto Get()
        {
            DataSet _ds = null;
            var Params = Param(new CompanyDto(), 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspCompany", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        CompanyDto result = new CompanyDto();
                        {
                            result.CompanyId = row["CompanyId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CompanyId"]);
                            result.CompanyCode = row["CompanyCode"].ToString();
                            result.CompanyName = row["CompanyName"].ToString();
                            result.CompanyGSTNo = row["CompanyGSTNo"].ToString();
                            result.CompanyWebsite = row["CompanyWebsite"].ToString();
                            result.CompanyMobileNo = row["CompanyMobileNo"].ToString();
                            result.CompanyEmailId = row["CompanyEmailId"].ToString();
                            result.CompanyFax = row["CompanyFax"].ToString();
                            result.CompanyCityName = row["CityName"].ToString();
                            result.CompanyStateName = row["StateName"].ToString();
                            result.CompanyPhone2 = row["CompanyPhone2"].ToString();
                            result.CompanyPhone1 = row["CompanyPhone1"].ToString();
                            result.CompanyStateId = row["CompanyStateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CompanyStateId"]);
                            result.CompanyCityId = row["CompanyCityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CompanyCityId"]);
                            result.CompanyAddress = row["CompanyAddress"].ToString();
                            result.CompanyZipCode = row["CompanyZipCode"].ToString();
                            result.CompanyBankId = row["CompanyBankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CompanyBankId"]);
                            result.CompanyAccountNo = row["CompanyAccountNo"].ToString();
                            result.CompanyIFSCode = row["CompanyIFSCode"].ToString();
                            result.CompanyBankBranch = row["CompanyBankBranch"].ToString();
                            result.CompanyLogo = row["CompanyLogo"].ToString();
                            result.CompanyIsActive = row["CompanyIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["CompanyIsActive"]);
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
        public bool UpdateNameInformation(CompanyDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspCompany", Params);

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
        public bool UpdateContactInformation(CompanyDto obj)
        {
            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspCompany", Params);

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
        public bool UpdateAddressInformation(CompanyDto obj)
        {
            var Params = Param(obj, 4);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspCompany", Params);

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
        public bool UpdateBankInformation(CompanyDto obj)
        {
            var Params = Param(obj, 5);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspCompany", Params);

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
        public bool UpdateLogoInformation(CompanyDto obj)
        {
            var Params = Param(obj, 6);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspCompany", Params);

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
    }
}