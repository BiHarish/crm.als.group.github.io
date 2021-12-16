using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ICWR.Data
{
    public class UserData 
    {
        SqlParameter[] Param(UserDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[62];
            Param[0] = new SqlParameter("@Id", SqlDbType.BigInt);
            Param[0].Value = obj.Id;

            Param[1] = new SqlParameter("@Code", SqlDbType.VarChar);
            Param[1].Value = obj.Code;

            Param[2] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[2].Value = obj.Name;

            Param[3] = new SqlParameter("@GuardianName", SqlDbType.NVarChar);
            Param[3].Value = obj.GuardianName;

            Param[4] = new SqlParameter("@Age", SqlDbType.Int);
            Param[4].Value = obj.Age;

            Param[5] = new SqlParameter("@Birthday", SqlDbType.Date);
            Param[5].Value = obj.Birthday;

            Param[6] = new SqlParameter("@Gender", SqlDbType.Char);
            Param[6].Value = obj.Gender;

            Param[7] = new SqlParameter("@MaritalStatus", SqlDbType.Char);
            Param[7].Value = obj.MaritalStatus;

            Param[8] = new SqlParameter("@BranchId", SqlDbType.Int);
            Param[8].Value = obj.BranchId;

            Param[9] = new SqlParameter("@Address", SqlDbType.NVarChar);
            Param[9].Value = obj.Address;

            Param[10] = new SqlParameter("@CityId", SqlDbType.BigInt);
            Param[10].Value = obj.CityId;

            Param[11] = new SqlParameter("@StateId", SqlDbType.BigInt);
            Param[11].Value = obj.StateId;

            Param[12] = new SqlParameter("@ZipCode", SqlDbType.VarChar);
            Param[12].Value = obj.ZipCode;

            Param[13] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
            Param[13].Value = obj.MobileNo;

            Param[14] = new SqlParameter("@EmailId", SqlDbType.VarChar);
            Param[14].Value = obj.EmailId;

            Param[15] = new SqlParameter("@AccountNo", SqlDbType.VarChar);
            Param[15].Value = obj.AccountNo;

            Param[16] = new SqlParameter("@BankId", SqlDbType.Int);
            Param[16].Value = obj.BankId;

            Param[17] = new SqlParameter("@BankAddress", SqlDbType.NVarChar);
            Param[17].Value = obj.BankAddress;

            Param[18] = new SqlParameter("@Ifscode", SqlDbType.VarChar);
            Param[18].Value = obj.Ifscode;

            Param[19] = new SqlParameter("@JoiningDate", SqlDbType.Date);
            Param[19].Value = obj.JoiningDate;

            Param[20] = new SqlParameter("@NomineeName", SqlDbType.NVarChar);
            Param[20].Value = obj.NomineeName;

            Param[21] = new SqlParameter("@NomineeRelationId", SqlDbType.Int);
            Param[21].Value = obj.NomineeRelationId;

            Param[22] = new SqlParameter("@NomineeZipCode", SqlDbType.VarChar);
            Param[22].Value = obj.NomineeZipCode;

            Param[23] = new SqlParameter("@NomineeCityId", SqlDbType.BigInt);
            Param[23].Value = obj.NomineeCityId;

            Param[24] = new SqlParameter("@NomineeAge", SqlDbType.Int);
            Param[24].Value = obj.NomineeAge;

            Param[25] = new SqlParameter("@NomineeAddress", SqlDbType.NVarChar);
            Param[25].Value = obj.NomineeAddress;

            Param[26] = new SqlParameter("@IntroducerCode", SqlDbType.VarChar);
            Param[26].Value = obj.IntroducerCode;

            Param[27] = new SqlParameter("@IntroducerId", SqlDbType.BigInt);
            Param[27].Value = obj.IntroducerId;

            Param[28] = new SqlParameter("@UplineCode", SqlDbType.VarChar);
            Param[28].Value = obj.UplineCode;

            Param[29] = new SqlParameter("@UplineID", SqlDbType.BigInt);
            Param[29].Value = obj.UplineID;

            Param[30] = new SqlParameter("@IsVerify", SqlDbType.Bit);
            Param[30].Value = obj.IsVerify;

            Param[31] = new SqlParameter("@VerifyBy", SqlDbType.BigInt);
            Param[31].Value = obj.VerifyBy;

            Param[32] = new SqlParameter("@VerifyDate", SqlDbType.Date);
            Param[32].Value = obj.VerifyDate;

            Param[33] = new SqlParameter("@ProductReceive", SqlDbType.Char);
            Param[33].Value = obj.ProductReceive;

            Param[34] = new SqlParameter("@PaymentReceive", SqlDbType.Char);
            Param[34].Value = obj.PaymentReceive;

            Param[35] = new SqlParameter("@ProductReceiveDate", SqlDbType.Date);
            Param[35].Value = obj.ProductReceiveDate;

            Param[36] = new SqlParameter("@Position", SqlDbType.Char);
            Param[36].Value = obj.Position;

            Param[37] = new SqlParameter("@UserTypeId", SqlDbType.Int);
            Param[37].Value = obj.UserTypeId;

            Param[38] = new SqlParameter("@RankId", SqlDbType.Int);
            Param[38].Value = obj.RankId;

            Param[39] = new SqlParameter("@Profile", SqlDbType.VarChar);
            Param[39].Value = obj.Profile;

            Param[40] = new SqlParameter("@Aadhar", SqlDbType.VarChar);
            Param[40].Value = obj.Aadhar;

            Param[41] = new SqlParameter("@AadharImage", SqlDbType.VarChar);
            Param[41].Value = obj.AadharImage;

            Param[42] = new SqlParameter("@DlNo", SqlDbType.VarChar);
            Param[42].Value = obj.DlNo;

            Param[43] = new SqlParameter("@DlNoImage", SqlDbType.VarChar);
            Param[43].Value = obj.DlNoImage;

            Param[44] = new SqlParameter("@VoterId", SqlDbType.VarChar);
            Param[44].Value = obj.VoterId;

            Param[45] = new SqlParameter("@VoterIdImage", SqlDbType.VarChar);
            Param[45].Value = obj.VoterIdImage;

            Param[46] = new SqlParameter("@Passport", SqlDbType.VarChar);
            Param[46].Value = obj.Passport;

            Param[47] = new SqlParameter("@PassportImage", SqlDbType.VarChar);
            Param[47].Value = obj.PassportImage;

            Param[48] = new SqlParameter("@PanCardNo", SqlDbType.VarChar);
            Param[48].Value = obj.PanCardNo;

            Param[49] = new SqlParameter("@PanCardImage", SqlDbType.VarChar);
            Param[49].Value = obj.PanCardImage;

            Param[50] = new SqlParameter("@PinNumber", SqlDbType.VarChar);
            Param[50].Value = obj.PinNumber;

            Param[51] = new SqlParameter("@PinId", SqlDbType.Int);
            Param[51].Value = obj.PinId;

            Param[52] = new SqlParameter("@PlanId", SqlDbType.Int);
            Param[52].Value = obj.PlanId;

            Param[53] = new SqlParameter("@IsActive", SqlDbType.Bit);
            Param[53].Value = obj.IsActive;

            Param[54] = new SqlParameter("@Password", SqlDbType.NVarChar);
            Param[54].Value = obj.Password;

            Param[55] = new SqlParameter("@BankImage", SqlDbType.VarChar);
            Param[55].Value = obj.BankImage;

            Param[56] = new SqlParameter("@Level", SqlDbType.Int);
            Param[56].Value = obj.Level;

            Param[57] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[57].Value = flag;

            Param[58] = new SqlParameter("@RptMgrID", SqlDbType.BigInt);
            Param[58].Value = obj.RptMgrID;

            Param[59] = new SqlParameter("@IsCRM", SqlDbType.Bit);
            Param[59].Value = obj.IsCRM;

            Param[60] = new SqlParameter("@ApprovalType", SqlDbType.NVarChar);
            Param[60].Value = obj.ApprovalType;

            Param[61] = new SqlParameter("@LocationID", SqlDbType.BigInt);
            Param[61].Value = obj.LocationID;

            return Param;
        }

        //Insert Procedures
        public UserDto Insert(UserDto obj)
        {
            DataSet _ds = null;
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspInsertMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        UserDto result = new UserDto();
                        {
                            result.MobileNo = row["MobileNo"].ToString();
                            result.Name = row["JoiningMessage"].ToString();
                            result.Id = row["ID"].ToDataConvertNullInt64();
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
        public UserDto GetTotalIntroducer(string Code)
        {
            Object Number = String.Empty;
            UserDto response = new UserDto();
            response.IntroducerCode = Code;
            var Params = Param(response, 2);
            if (Params != null)
            {
                try
                {
                    Number = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "UspInsertMember", Params);
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
                finally
                {

                }
                if (!String.IsNullOrEmpty(Number.ToString()))
                {
                    response = new UserDto()
                    {
                        Position = Number.ToString()
                    };
                }
                return response;
            }
            return response;
        }
        public bool IsBothSideAvail(string type, string Code)
        {
            UserDto _user = new UserDto();
            _user.IntroducerCode = Code;
            _user.Position = type;
            var Params = Param(_user, 3);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "UspInsertMember", Params);
                    if ((int)i > 0)
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
        public UserDto GetCurrentPosition(string Code)
        {
            Object Number = String.Empty;
            UserDto response = new UserDto();
            response.IntroducerCode = Code;
            var Params = Param(response, 4);
            if (Params != null)
            {
                try
                {
                    Number = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "UspInsertMember", Params);
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
                finally
                {

                }
                if (!String.IsNullOrEmpty((string)Number))
                {
                    response = new UserDto()
                    {
                        Position = (string)Number
                    };
                }
                return response;
            }
            return response;
        }
        public LovelyUserPermission LoginAuthentication(UserDto data)
        {
            DataSet _ds = null;
            var Params = Param(data, 5); 
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspInsertMember", Params);

                    LovelyUserPermission _loveUserPermission = null;
                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        _loveUserPermission = new LovelyUserPermission();

                        DataRow row = _ds.Tables[0].Rows[0];
                        UserDto result = new UserDto();
                        { 
                            result.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            result.Code = row["Code"].ToString();
                            result.Name = row["Name"].ToString();
                            result.GuardianName = row["GuardianName"].ToString();
                            result.Age = row["Age"] == DBNull.Value ? 0 : Convert.ToInt32(row["Age"]);
                            result.Birthday = row["Birthday"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["Birthday"]);
                            result.Gender = row["Gender"].ToString();
                            result.MaritalStatus = row["MaritalStatus"].ToString();
                            result.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                            result.Address = row["Address"].ToString();
                            result.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            result.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            result.ZipCode = row["ZipCode"].ToString();
                            result.MobileNo = row["MobileNo"].ToString();
                            result.EmailId = row["EmailId"].ToString();
                            result.AccountNo = row["AccountNo"].ToString();
                            result.Ifscode = row["Ifscode"].ToString();
                            result.BankAddress = row["BankAddress"].ToString();
                            result.BankId = row["BankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BankId"]);
                            result.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);
                            result.NomineeName = row["NomineeName"].ToString();
                            result.NomineeRelationId = row["NomineeRelationId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeRelationId"]);
                            result.NomineeZipCode = row["NomineeZipCode"].ToString();
                            result.NomineeAddress = row["NomineeAddress"].ToString();
                            result.NomineeAge = row["NomineeAge"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeAge"]);
                            result.NomineeCityId = row["NomineeCityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeCityId"]);
                            result.IntroducerCode = row["IntroducerCode"].ToString();
                            result.IntroducerId = row["IntroducerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["IntroducerId"]);
                            result.UplineCode = row["UplineCode"].ToString();
                            result.UplineID = row["UplineID"] == DBNull.Value ? 0 : Convert.ToInt32(row["UplineID"]);
                            result.Password = row["Password"].ToString();
                            result.IsVerify = row["IsVerify"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVerify"]);
                            result.VerifyDate = row["VerifyDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["VerifyDate"]);
                            result.VerifyBy = row["VerifyBy"] == DBNull.Value ? 0 : Convert.ToInt64(row["VerifyBy"]);
                            result.PaymentReceive = row["PaymentReceive"].ToString();
                            result.ProductReceive = row["ProductReceive"].ToString();
                            result.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            result.Position = row["Position"].ToString();
                            result.UserTypeId = row["UserTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserTypeId"]);
                            result.RankId = row["RankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RankId"]);
                            result.Profile = row["Profile"].ToString();
                            result.Aadhar = row["Aadhar"].ToString();
                            result.AadharImage = row["AadharImage"].ToString();
                            result.VoterId = row["VoterId"].ToString();
                            result.VoterIdImage = row["VoterIdImage"].ToString();
                            result.DlNo = row["DlNo"].ToString();
                            result.DlNoImage = row["DlNoImage"].ToString();
                            result.Passport = row["Passport"].ToString();
                            result.PassportImage = row["PassportImage"].ToString();
                            result.PanCardNo = row["PanCardNo"].ToString();
                            result.PanCardImage = row["PanCardImage"].ToString();
                            result.PinNumber = row["PinNumber"].ToString();
                            result.BankImage = row["BankImage"].ToString();
                            result.UserType = row["RoleName"].ToString();
                            result.Level = row["Level"] == DBNull.Value ? 0 : Convert.ToInt32(row["Level"]);
                            result.PinId = row["PinId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinId"]);
                            result.PlanId = row["PlanId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PlanId"]);
                            result.IsActive = row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["IsActive"]);
                            result.IsScreenLock = false;
                            result.Location = row["Location"].ToString();
                            result.LocationID = row["LocationID"].ToDataConvertNullInt64();
                        }
                        _loveUserPermission.User = result;

                        if (_ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)
                        {
                            DataRowCollection rows = _ds.Tables[1].Rows;
                            List<PermissionMasterDto> PermissionResults = new List<PermissionMasterDto>();

                            foreach (DataRow row1 in rows)
                            {
                                PermissionMasterDto obj = new PermissionMasterDto();
                                obj.PermissionMasterMenuId = row1["PermissionMasterMenuId"] == DBNull.Value ? 0 : Convert.ToInt64(row1["PermissionMasterMenuId"]);
                                obj.PermissionMasterMenuShow = row1["PermissionMastermenushow"] == DBNull.Value ? false : Convert.ToBoolean(row1["PermissionMastermenushow"]);
                                obj.PermissionMasterView = row1["PermissionMasterview"] == DBNull.Value ? false : Convert.ToBoolean(row1["PermissionMasterview"]);
                                obj.PermissionMasterAdd = row1["PermissionMasteradd"] == DBNull.Value ? false : Convert.ToBoolean(row1["PermissionMasteradd"]);
                                obj.PermissionMasterUpdate = row1["PermissionMasterupdate"] == DBNull.Value ? false : Convert.ToBoolean(row1["PermissionMasterupdate"]);
                                obj.PermissionMasterDelete = row1["PermissionMasterdelete"] == DBNull.Value ? false : Convert.ToBoolean(row1["PermissionMasterdelete"]);
                                obj.PermissionMasterPrint = row1["PermissionMasterprint"] == DBNull.Value ? false : Convert.ToBoolean(row1["PermissionMasterprint"]);
                                obj.PermissionMasterSelf = row1["PermissionMasterself"] == DBNull.Value ? false : Convert.ToBoolean(row1["PermissionMasterself"]);
                                PermissionResults.Add(obj);
                            }
                            _loveUserPermission.Permissions = PermissionResults;
                        }

                        if (_ds.Tables.Count > 2 && _ds.Tables[2].Rows.Count > 0)
                        {
                            DataRowCollection rows = _ds.Tables[2].Rows;
                            List<PermissionCompanyDto> PermissionCompanyResults = new List<PermissionCompanyDto>();

                            foreach (DataRow row1 in rows)
                            {
                                PermissionCompanyDto obj = new PermissionCompanyDto();
                                //obj.PermissionCompanyId = row1["PermissionCompanyId"] == DBNull.Value ? 0 : Convert.ToInt64(row1["PermissionCompanyId"]);
                                //obj.PermissionCompanyCompanyId = row1["PermissionCompanyCompanyId"] == DBNull.Value ? 0 : Convert.ToInt64(row1["PermissionCompanyCompanyId"]);
                                //obj.PermissionCompanyUserRoleId = row1["PermissionCompanyUserRoleId"] == DBNull.Value ? 0 : Convert.ToInt64(row1["PermissionCompanyUserRoleId"]);
                                obj.CompanyId = row1["CargoWiseCompanyId"] == DBNull.Value ? 0 : Convert.ToInt64(row1["CargoWiseCompanyId"]);
                                obj.CompanyName = row1["CargoWiseName"].ToString();
                                obj.CompanyCode = row1["CargoWiseCode"].ToString();
                                obj.CompanyUniqueNumber = row1["CargoWiseUniqueNumber"].ToString();
                                obj.Combine_CompanyUnique_USD_Code = row1["CargoWiseUniqueNumber"].ToString() + "|" + row1["CargoWiseUSDRate"].ToString() + "|" + row1["CargoWiseCode"].ToString() + "|" + row1["Mul_Divide"].ToString(); 
                                //obj.PermissionCompanyView = row1["PermissionCompanyview"] == DBNull.Value ? false : Convert.ToBoolean(row1["PermissionCompanyview"]);
                                PermissionCompanyResults.Add(obj);
                            }
                            _loveUserPermission.PermissionsCompany = PermissionCompanyResults;
                        }

                        if (_ds.Tables.Count > 3 && _ds.Tables[3].Rows.Count > 0)
                        {
                            DataRowCollection rows = _ds.Tables[3].Rows;
                            List<CurrencyMasterDto> PermissionCuurrencyMasterResults = new List<CurrencyMasterDto>();

                            foreach (DataRow row1 in rows)
                            {
                                CurrencyMasterDto obj = new CurrencyMasterDto();
                                obj.CurrencyName = row1["Currency"].ToString();
                                obj.CurrencyPrice = row1["Value"].ToString();
                                obj.CurrencyPrice1 = row1["Value"].ToString() +"|" + row1["Mul_Divide"].ToString();
                                //obj.PermissionSchemaMasterSchemaMasterId = row1["PermissionSchemaMasterMenuId"] == DBNull.Value ? 0 : Convert.ToInt64(row1["PermissionSchemaMasterMenuId"]);
                                //obj.PermissionSchemaMasterId = row1["PermissionSchemaMasterId"] == DBNull.Value ? 0 : Convert.ToInt64(row1["PermissionSchemaMasterId"]);
                                //obj.PermissionSchemaMasterSchemaMasterId = row1["PermissionSchemaMasterSchemaMasterId"] == DBNull.Value ? 0 : Convert.ToInt64(row1["PermissionSchemaMasterSchemaMasterId"]);
                                //obj.PermissionSchemaMasterUserRoleId = row1["PermissionSchemaMasterUserRoleId"] == DBNull.Value ? 0 : Convert.ToInt64(row1["PermissionSchemaMasterUserRoleId"]);
                                //obj.SchemaMasterId = row1["CargoWiseSchemaMasterId"] == DBNull.Value ? 0 : Convert.ToInt64(row1["CargoWiseSchemaMasterId"]);
                                //obj.SchemaMasterName = row1["CargoWiseCompanyName"].ToString();
                                //obj.CompanyCode = row1["CargoWiseCompanyCode"].ToString();
                                ////obj.PermissionCompanyView = row1["PermissionCompanyview"] == DBNull.Value ? false : Convert.ToBoolean(row1["PermissionCompanyview"]);
                                PermissionCuurrencyMasterResults.Add(obj);
                            }
                            _loveUserPermission.PermissionsCurrencyMaster = PermissionCuurrencyMasterResults;
                        }
                        return _loveUserPermission;
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
         
        //Normal Procedures
        public UserDto GetByCode(string Code)
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();
            _user.Code = Code;
            var Params = Param(_user, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        UserDto result = new UserDto();
                        {
                            result.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            result.Code = row["Code"].ToString();
                            result.Name = row["Name"].ToString();
                            result.GuardianName = row["GuardianName"].ToString();
                            result.Age = row["Age"] == DBNull.Value ? 0 : Convert.ToInt32(row["Age"]);
                            result.Birthday = row["Birthday"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["Birthday"]);
                            result.Gender = row["Gender"].ToString();
                            result.MaritalStatus = row["MaritalStatus"].ToString();
                            result.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                            result.Address = row["Address"].ToString();
                            result.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            result.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            result.ZipCode = row["ZipCode"].ToString();
                            result.MobileNo = row["MobileNo"].ToString();
                            result.EmailId = row["EmailId"].ToString();
                            result.AccountNo = row["AccountNo"].ToString();
                            result.Ifscode = row["Ifscode"].ToString();
                            result.BankAddress = row["BankAddress"].ToString();
                            result.BankId = row["BankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BankId"]);
                            result.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);
                            result.NomineeName = row["NomineeName"].ToString();
                            result.NomineeRelationId = row["NomineeRelationId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeRelationId"]);
                            result.NomineeZipCode = row["NomineeZipCode"].ToString();
                            result.NomineeAddress = row["NomineeAddress"].ToString();
                            result.NomineeAge = row["NomineeAge"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeAge"]);
                            result.NomineeCityId = row["NomineeCityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeCityId"]);
                            result.IntroducerCode = row["IntroducerCode"].ToString();
                            result.IntroducerId = row["IntroducerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["IntroducerId"]);
                            result.UplineCode = row["UplineCode"].ToString();
                            result.UplineID = row["UplineID"] == DBNull.Value ? 0 : Convert.ToInt32(row["UplineID"]);
                            result.Password = row["Password"].ToString();
                            result.IsVerify = row["IsVerify"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVerify"]);
                            result.VerifyDate = row["VerifyDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["VerifyDate"]);
                            result.VerifyBy = row["VerifyBy"] == DBNull.Value ? 0 : Convert.ToInt64(row["VerifyBy"]);
                            result.PaymentReceive = row["PaymentReceive"].ToString();
                            result.ProductReceive = row["ProductReceive"].ToString();
                            result.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            result.Position = row["Position"].ToString();
                            result.UserTypeId = row["UserTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserTypeId"]);
                            result.RankId = row["RankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RankId"]);
                            result.Profile = row["Profile"].ToString();
                            result.Aadhar = row["Aadhar"].ToString();
                            result.AadharImage = row["AadharImage"].ToString();
                            result.VoterId = row["VoterId"].ToString();
                            result.VoterIdImage = row["VoterIdImage"].ToString();
                            result.DlNo = row["DlNo"].ToString();
                            result.DlNoImage = row["DlNoImage"].ToString();
                            result.Passport = row["Passport"].ToString();
                            result.PassportImage = row["PassportImage"].ToString();
                            result.PanCardNo = row["PanCardNo"].ToString();
                            result.PanCardImage = row["PanCardImage"].ToString();
                            result.PinNumber = row["PinNumber"].ToString();
                            result.BankImage = row["BankImage"].ToString();
                            result.Level = row["Level"] == DBNull.Value ? 0 : Convert.ToInt32(row["Level"]);
                            result.PinId = row["PinId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinId"]);
                            result.PlanId = row["PlanId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PlanId"]);
                            result.IsActive = row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["IsActive"]);
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
        public UserDto GetByCardCode(string Code)
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();
            _user.BankImage = Code;
            var Params = Param(_user, 17);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        UserDto result = new UserDto();
                        {
                            result.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            result.Code = row["Code"].ToString();
                            result.Name = row["Name"].ToString();
                            result.GuardianName = row["GuardianName"].ToString();
                            result.Age = row["Age"] == DBNull.Value ? 0 : Convert.ToInt32(row["Age"]);
                            result.Birthday = row["Birthday"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["Birthday"]);
                            result.Gender = row["Gender"].ToString();
                            result.MaritalStatus = row["MaritalStatus"].ToString();
                            result.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                            result.Address = row["Address"].ToString();
                            result.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            result.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            result.ZipCode = row["ZipCode"].ToString();
                            result.MobileNo = row["MobileNo"].ToString();
                            result.EmailId = row["EmailId"].ToString();
                            result.AccountNo = row["AccountNo"].ToString();
                            result.Ifscode = row["Ifscode"].ToString();
                            result.BankAddress = row["BankAddress"].ToString();
                            result.BankId = row["BankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BankId"]);
                            result.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);
                            result.NomineeName = row["NomineeName"].ToString();
                            result.NomineeRelationId = row["NomineeRelationId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeRelationId"]);
                            result.NomineeZipCode = row["NomineeZipCode"].ToString();
                            result.NomineeAddress = row["NomineeAddress"].ToString();
                            result.NomineeAge = row["NomineeAge"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeAge"]);
                            result.NomineeCityId = row["NomineeCityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeCityId"]);
                            result.IntroducerCode = row["IntroducerCode"].ToString();
                            result.IntroducerId = row["IntroducerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["IntroducerId"]);
                            result.UplineCode = row["UplineCode"].ToString();
                            result.UplineID = row["UplineID"] == DBNull.Value ? 0 : Convert.ToInt32(row["UplineID"]);
                            result.Password = row["Password"].ToString();
                            result.IsVerify = row["IsVerify"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVerify"]);
                            result.VerifyDate = row["VerifyDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["VerifyDate"]);
                            result.VerifyBy = row["VerifyBy"] == DBNull.Value ? 0 : Convert.ToInt64(row["VerifyBy"]);
                            result.PaymentReceive = row["PaymentReceive"].ToString();
                            result.ProductReceive = row["ProductReceive"].ToString();
                            result.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            result.Position = row["Position"].ToString();
                            result.UserTypeId = row["UserTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserTypeId"]);
                            result.RankId = row["RankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RankId"]);
                            result.Profile = row["Profile"].ToString();
                            result.Aadhar = row["Aadhar"].ToString();
                            result.AadharImage = row["AadharImage"].ToString();
                            result.VoterId = row["VoterId"].ToString();
                            result.VoterIdImage = row["VoterIdImage"].ToString();
                            result.DlNo = row["DlNo"].ToString();
                            result.DlNoImage = row["DlNoImage"].ToString();
                            result.Passport = row["Passport"].ToString();
                            result.PassportImage = row["PassportImage"].ToString();
                            result.PanCardNo = row["PanCardNo"].ToString();
                            result.PanCardImage = row["PanCardImage"].ToString();
                            result.PinNumber = row["PinNumber"].ToString();
                            result.BankImage = row["BankImage"].ToString();
                            result.Level = row["Level"] == DBNull.Value ? 0 : Convert.ToInt32(row["Level"]);
                            result.PinId = row["PinId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinId"]);
                            result.PlanId = row["PlanId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PlanId"]);
                            result.IsActive = row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["IsActive"]);
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
        public bool Update(UserDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

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
        public UserDto GetById(long id)
        {
            DataSet _ds = null;

            UserDto _user = new UserDto();
            _user.Id = id;
            var Params = Param(_user, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        UserDto result = new UserDto();
                        {
                            result.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            result.Code = row["Code"].ToString();
                            result.Name = row["Name"].ToString();
                            result.GuardianName = row["GuardianName"].ToString();
                            result.Age = row["Age"] == DBNull.Value ? 0 : Convert.ToInt32(row["Age"]);
                            result.Birthday = row["Birthday"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["Birthday"]);
                            result.Gender = row["Gender"].ToString();
                            result.MaritalStatus = row["MaritalStatus"].ToString();
                            result.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                            result.Address = row["Address"].ToString();
                            result.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            result.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            result.CityName = row["CityName"].ToString();
                            result.StateName = row["StateName"].ToString();
                            result.ZipCode = row["ZipCode"].ToString();
                            result.MobileNo = row["MobileNo"].ToString();
                            result.EmailId = row["EmailId"].ToString();
                            result.AccountNo = row["AccountNo"].ToString();
                            result.Ifscode = row["Ifscode"].ToString();
                            result.BankAddress = row["BankAddress"].ToString();
                            result.BankId = row["BankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BankId"]);
                            result.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);
                            result.NomineeName = row["NomineeName"].ToString();
                            result.NomineeRelationId = row["NomineeRelationId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeRelationId"]);
                            result.NomineeZipCode = row["NomineeZipCode"].ToString();
                            result.NomineeAddress = row["NomineeAddress"].ToString();
                            result.NomineeAge = row["NomineeAge"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeAge"]);
                            result.NomineeCityId = row["NomineeCityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeCityId"]);
                            result.IntroducerCode = row["IntroducerCode"].ToString();
                            result.IntroducerId = row["IntroducerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["IntroducerId"]);
                            result.UplineCode = row["UplineCode"].ToString();
                            result.UplineID = row["UplineID"] == DBNull.Value ? 0 : Convert.ToInt32(row["UplineID"]);
                            result.Password = row["Password"].ToString();
                            result.IsVerify = row["IsVerify"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVerify"]);
                            result.VerifyDate = row["VerifyDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["VerifyDate"]);
                            result.VerifyBy = row["VerifyBy"] == DBNull.Value ? 0 : Convert.ToInt64(row["VerifyBy"]);
                            result.PaymentReceive = row["PaymentReceive"].ToString();
                            result.ProductReceive = row["ProductReceive"].ToString();
                            result.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            result.Position = row["Position"].ToString();
                            result.UserTypeId = row["UserTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserTypeId"]);
                            result.RankId = row["RankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RankId"]);
                            result.Profile = row["Profile"].ToString();
                            result.Aadhar = row["Aadhar"].ToString();
                            result.AadharImage = row["AadharImage"].ToString();
                            result.VoterId = row["VoterId"].ToString();
                            result.VoterIdImage = row["VoterIdImage"].ToString();
                            result.DlNo = row["DlNo"].ToString();
                            result.DlNoImage = row["DlNoImage"].ToString();
                            result.Passport = row["Passport"].ToString();
                            result.PassportImage = row["PassportImage"].ToString();
                            result.PanCardNo = row["PanCardNo"].ToString();
                            result.PanCardImage = row["PanCardImage"].ToString();
                            result.PinNumber = row["PinNumber"].ToString();
                            result.BankImage = row["BankImage"].ToString();
                            result.Level = row["Level"] == DBNull.Value ? 0 : Convert.ToInt32(row["Level"]);
                            result.PinId = row["PinId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinId"]);
                            result.PlanId = row["PlanId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PlanId"]);
                            result.IsActive = row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["IsActive"]);
                            result.RptMgrID = row["RptMgrID"].ToDataConvertNullInt64();
                            result.IsCRM = row["IsCRM"].ToDataConvertBool();
                            result.LocationID = row["LocationID"].ToDataConvertNullInt64();
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
        public IList<UserDto> GetAll(int UserType, bool Type)
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();
            _user.IsActive = Type;
            _user.UserTypeId = UserType;
            var Params = Param(_user, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            obj.Code = row["Code"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.GuardianName = row["GuardianName"].ToString();
                            obj.Age = row["Age"] == DBNull.Value ? 0 : Convert.ToInt32(row["Age"]);
                            obj.Birthday = row["Birthday"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["Birthday"]);
                            obj.Gender = row["Gender"].ToString();
                            obj.MaritalStatus = row["MaritalStatus"].ToString();
                            obj.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                            obj.Address = row["Address"].ToString();
                            obj.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            obj.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            obj.ZipCode = row["ZipCode"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.EmailId = row["EmailId"].ToString();
                            obj.AccountNo = row["AccountNo"].ToString();
                            obj.Ifscode = row["Ifscode"].ToString();
                            obj.BankAddress = row["BankAddress"].ToString();
                            obj.BankId = row["BankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BankId"]);
                            obj.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);
                            obj.NomineeName = row["NomineeName"].ToString();
                            obj.NomineeRelationId = row["NomineeRelationId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeRelationId"]);
                            obj.NomineeZipCode = row["NomineeZipCode"].ToString();
                            obj.NomineeAddress = row["NomineeAddress"].ToString();
                            obj.NomineeAge = row["NomineeAge"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeAge"]);
                            obj.NomineeCityId = row["NomineeCityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeCityId"]);
                            obj.IntroducerCode = row["IntroducerCode"].ToString();
                            obj.IntroducerId = row["IntroducerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["IntroducerId"]);
                            obj.UplineCode = row["UplineCode"].ToString();
                            obj.UplineID = row["UplineID"] == DBNull.Value ? 0 : Convert.ToInt32(row["UplineID"]);
                            obj.Password = row["Password"].ToString();
                            obj.IsVerify = row["IsVerify"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVerify"]);
                            obj.VerifyDate = row["VerifyDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["VerifyDate"]);
                            obj.VerifyBy = row["VerifyBy"] == DBNull.Value ? 0 : Convert.ToInt64(row["VerifyBy"]);
                            obj.PaymentReceive = row["PaymentReceive"].ToString();
                            obj.ProductReceive = row["ProductReceive"].ToString();
                            obj.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            obj.Position = row["Position"].ToString();
                            obj.UserTypeId = row["UserTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserTypeId"]);
                            obj.RankId = row["RankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RankId"]);
                            obj.Profile = row["Profile"].ToString();
                            obj.Aadhar = row["Aadhar"].ToString();
                            obj.AadharImage = row["AadharImage"].ToString();
                            obj.VoterId = row["VoterId"].ToString();
                            obj.VoterIdImage = row["VoterIdImage"].ToString();
                            obj.DlNo = row["DlNo"].ToString();
                            obj.DlNoImage = row["DlNoImage"].ToString();
                            obj.Passport = row["Passport"].ToString();
                            obj.PassportImage = row["PassportImage"].ToString();
                            obj.PanCardNo = row["PanCardNo"].ToString();
                            obj.PanCardImage = row["PanCardImage"].ToString();
                            obj.PinNumber = row["PinNumber"].ToString();
                            obj.BankImage = row["BankImage"].ToString();
                            obj.Level = row["Level"] == DBNull.Value ? 0 : Convert.ToInt32(row["Level"]);
                            obj.PinId = row["PinId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinId"]);
                            obj.PlanId = row["PlanId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PlanId"]);
                            obj.IsActive = row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["IsActive"]);

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
        public IList<UserDto> GetByVerifyCredentials(UserDto data)
        {
            DataSet _ds = null;
            var Params = Param(data, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            obj.Code = row["Code"].ToString();
                            obj.UserType = row["RoleName"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.GuardianName = row["GuardianName"].ToString();
                            obj.Age = row["Age"] == DBNull.Value ? 0 : Convert.ToInt32(row["Age"]);
                            obj.Birthday = row["Birthday"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["Birthday"]);
                            obj.Gender = row["Gender"].ToString();
                            obj.MaritalStatus = row["MaritalStatus"].ToString();
                            obj.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                            obj.Address = row["Address"].ToString();
                            obj.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            obj.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            obj.ZipCode = row["ZipCode"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.EmailId = row["EmailId"].ToString();
                            obj.AccountNo = row["AccountNo"].ToString();
                            obj.Ifscode = row["Ifscode"].ToString();
                            obj.BankAddress = row["BankAddress"].ToString();
                            obj.BankId = row["BankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BankId"]);
                            obj.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);
                            obj.NomineeName = row["NomineeName"].ToString();
                            obj.NomineeRelationId = row["NomineeRelationId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeRelationId"]);
                            obj.NomineeZipCode = row["NomineeZipCode"].ToString();
                            obj.NomineeAddress = row["NomineeAddress"].ToString();
                            obj.NomineeAge = row["NomineeAge"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeAge"]);
                            obj.NomineeCityId = row["NomineeCityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeCityId"]);
                            obj.IntroducerCode = row["IntroducerCode"].ToString();
                            obj.IntroducerId = row["IntroducerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["IntroducerId"]);
                            obj.UplineCode = row["UplineCode"].ToString();
                            obj.UplineID = row["UplineID"] == DBNull.Value ? 0 : Convert.ToInt32(row["UplineID"]);
                            obj.Password = row["Password"].ToString();
                            obj.IsVerify = row["IsVerify"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVerify"]);
                            obj.VerifyDate = row["VerifyDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["VerifyDate"]);
                            obj.VerifyBy = row["VerifyBy"] == DBNull.Value ? 0 : Convert.ToInt64(row["VerifyBy"]);
                            obj.PaymentReceive = row["PaymentReceive"].ToString();
                            obj.ProductReceive = row["ProductReceive"].ToString();
                            obj.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            obj.Position = row["Position"].ToString();
                            obj.UserTypeId = row["UserTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserTypeId"]);
                            obj.RankId = row["RankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RankId"]);
                            obj.Profile = row["Profile"].ToString();
                            obj.Aadhar = row["Aadhar"].ToString();
                            obj.AadharImage = row["AadharImage"].ToString();
                            obj.VoterId = row["VoterId"].ToString();
                            obj.VoterIdImage = row["VoterIdImage"].ToString();
                            obj.DlNo = row["DlNo"].ToString();
                            obj.DlNoImage = row["DlNoImage"].ToString();
                            obj.Passport = row["Passport"].ToString();
                            obj.PassportImage = row["PassportImage"].ToString();
                            obj.PanCardNo = row["PanCardNo"].ToString();
                            obj.PanCardImage = row["PanCardImage"].ToString();
                            obj.PinNumber = row["PinNumber"].ToString();
                            obj.BankImage = row["BankImage"].ToString();
                            obj.Level = row["Level"] == DBNull.Value ? 0 : Convert.ToInt32(row["Level"]);
                            obj.PinId = row["PinId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinId"]);
                            obj.PlanId = row["PlanId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PlanId"]);
                            obj.IsActive = row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["IsActive"]);

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
        public bool IsVerifyOnOff(UserDto obj)
        {
            var Params = Param(obj, 6);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

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
        public IList<UserDto> GetByActiveCredentials(UserDto data)
        {
            DataSet _ds = null;
            var Params = Param(data, 7);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            obj.Code = row["Code"].ToString();
                            obj.UserType = row["RoleName"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.GuardianName = row["GuardianName"].ToString();
                            obj.Age = row["Age"] == DBNull.Value ? 0 : Convert.ToInt32(row["Age"]);
                            obj.Birthday = row["Birthday"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["Birthday"]);
                            obj.Gender = row["Gender"].ToString();
                            obj.MaritalStatus = row["MaritalStatus"].ToString();
                            obj.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                            obj.Address = row["Address"].ToString();
                            obj.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            obj.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            obj.ZipCode = row["ZipCode"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.EmailId = row["EmailId"].ToString();
                            obj.AccountNo = row["AccountNo"].ToString();
                            obj.Ifscode = row["Ifscode"].ToString();
                            obj.BankAddress = row["BankAddress"].ToString();
                            obj.BankId = row["BankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BankId"]);
                            obj.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);
                            obj.NomineeName = row["NomineeName"].ToString();
                            obj.NomineeRelationId = row["NomineeRelationId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeRelationId"]);
                            obj.NomineeZipCode = row["NomineeZipCode"].ToString();
                            obj.NomineeAddress = row["NomineeAddress"].ToString();
                            obj.NomineeAge = row["NomineeAge"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeAge"]);
                            obj.NomineeCityId = row["NomineeCityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeCityId"]);
                            obj.IntroducerCode = row["IntroducerCode"].ToString();
                            obj.IntroducerId = row["IntroducerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["IntroducerId"]);
                            obj.UplineCode = row["UplineCode"].ToString();
                            obj.UplineID = row["UplineID"] == DBNull.Value ? 0 : Convert.ToInt32(row["UplineID"]);
                            obj.Password = row["Password"].ToString();
                            obj.IsVerify = row["IsVerify"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVerify"]);
                            obj.VerifyDate = row["VerifyDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["VerifyDate"]);
                            obj.VerifyBy = row["VerifyBy"] == DBNull.Value ? 0 : Convert.ToInt64(row["VerifyBy"]);
                            obj.PaymentReceive = row["PaymentReceive"].ToString();
                            obj.ProductReceive = row["ProductReceive"].ToString();
                            obj.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            obj.Position = row["Position"].ToString();
                            obj.UserTypeId = row["UserTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserTypeId"]);
                            obj.RankId = row["RankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RankId"]);
                            obj.Profile = row["Profile"].ToString();
                            obj.Aadhar = row["Aadhar"].ToString();
                            obj.AadharImage = row["AadharImage"].ToString();
                            obj.VoterId = row["VoterId"].ToString();
                            obj.VoterIdImage = row["VoterIdImage"].ToString();
                            obj.DlNo = row["DlNo"].ToString();
                            obj.DlNoImage = row["DlNoImage"].ToString();
                            obj.Passport = row["Passport"].ToString();
                            obj.PassportImage = row["PassportImage"].ToString();
                            obj.PanCardNo = row["PanCardNo"].ToString();
                            obj.PanCardImage = row["PanCardImage"].ToString();
                            obj.PinNumber = row["PinNumber"].ToString();
                            obj.BankImage = row["BankImage"].ToString();
                            obj.Level = row["Level"] == DBNull.Value ? 0 : Convert.ToInt32(row["Level"]);
                            obj.PinId = row["PinId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinId"]);
                            obj.PlanId = row["PlanId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PlanId"]);
                            obj.IsActive = row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["IsActive"]);

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
        public bool IsActiveOnOff(UserDto obj)
        {
            var Params = Param(obj, 8);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

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
        public bool UpdateImage(UserDto obj)
        {
            var Params = Param(obj, 9);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);
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
        public bool UpdateBank(UserDto obj)
        {
            var Params = Param(obj, 10);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);
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
        public bool UpdateDocuments(UserDto obj)
        {
            var Params = Param(obj, 11);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);
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
        public bool ChangePassword(UserDto obj)
        {
            var Params = Param(obj, 12);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);
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
        public IList<UserDto> GetRandomMessageData(UserDto data)
        {
            DataSet _ds = null;
            var Params = Param(data, 13);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            obj.Code = row["Code"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();

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
        public IList<UserDto> GetBirthDayData()
        {
            DataSet _ds = null;
            UserDto data = new UserDto();
            data.Birthday = Utility.Utility.GetIndianDateTime();
            var Params = Param(data, 14);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            obj.Code = row["Code"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.Address = row["MSG"].ToString();

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
        public IList<UserDto> GetKitNotAllocate(UserDto data)
        {
            DataSet _ds = null;
            var Params = Param(data, 15);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            obj.Code = row["Code"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.IntroducerCode = row["IntroducerCode"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.UserType = row["RoleName"].ToString();
                            obj.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            obj.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);

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
        public bool SetKitReceive(UserDto obj)
        {
            var Params = Param(obj, 16);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);
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
        public IList<UserDto> GetLevelStatus(string Code)
        {
            DataSet _ds = null;

            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@Code", SqlDbType.VarChar);
            Param[0].Value = Code;

            if (Param != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspLevelCount", Param);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.Code = row["Code"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.IntroducerCode = row["IntroducerCode"].ToString();
                            obj.DirectCount = row["DirectCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["DirectCount"]);
                            obj.TeamCount = row["TeamCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["TeamCount"]);
                            obj.Level = row["level"] == DBNull.Value ? 0 : Convert.ToInt32(row["level"]);

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
        public IList<UserDto> GetAllByCategory(int UserType, int? category)
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();
            _user.PlanId = category;
            _user.UserTypeId = UserType;
            var Params = Param(_user, 18);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            obj.Code = row["Code"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.GuardianName = row["GuardianName"].ToString();
                            obj.Age = row["Age"] == DBNull.Value ? 0 : Convert.ToInt32(row["Age"]);
                            obj.Birthday = row["Birthday"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["Birthday"]);
                            obj.Gender = row["Gender"].ToString();
                            obj.MaritalStatus = row["MaritalStatus"].ToString();
                            obj.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                            obj.Address = row["Address"].ToString();
                            obj.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            obj.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            obj.ZipCode = row["ZipCode"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.EmailId = row["EmailId"].ToString();
                            obj.AccountNo = row["AccountNo"].ToString();
                            obj.Ifscode = row["Ifscode"].ToString();
                            obj.BankAddress = row["BankAddress"].ToString();
                            obj.BankId = row["BankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BankId"]);
                            obj.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);
                            obj.NomineeName = row["NomineeName"].ToString();
                            obj.NomineeRelationId = row["NomineeRelationId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeRelationId"]);
                            obj.NomineeZipCode = row["NomineeZipCode"].ToString();
                            obj.NomineeAddress = row["NomineeAddress"].ToString();
                            obj.NomineeAge = row["NomineeAge"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeAge"]);
                            obj.NomineeCityId = row["NomineeCityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeCityId"]);
                            obj.IntroducerCode = row["IntroducerCode"].ToString();
                            obj.IntroducerId = row["IntroducerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["IntroducerId"]);
                            obj.UplineCode = row["UplineCode"].ToString();
                            obj.UplineID = row["UplineID"] == DBNull.Value ? 0 : Convert.ToInt32(row["UplineID"]);
                            obj.Password = row["Password"].ToString();
                            obj.IsVerify = row["IsVerify"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVerify"]);
                            obj.VerifyDate = row["VerifyDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["VerifyDate"]);
                            obj.VerifyBy = row["VerifyBy"] == DBNull.Value ? 0 : Convert.ToInt64(row["VerifyBy"]);
                            obj.PaymentReceive = row["PaymentReceive"].ToString();
                            obj.ProductReceive = row["ProductReceive"].ToString();
                            obj.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            obj.Position = row["Position"].ToString();
                            obj.UserTypeId = row["UserTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserTypeId"]);
                            obj.RankId = row["RankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RankId"]);
                            obj.Profile = row["Profile"].ToString();
                            obj.Aadhar = row["Aadhar"].ToString();
                            obj.AadharImage = row["AadharImage"].ToString();
                            obj.VoterId = row["VoterId"].ToString();
                            obj.VoterIdImage = row["VoterIdImage"].ToString();
                            obj.DlNo = row["DlNo"].ToString();
                            obj.DlNoImage = row["DlNoImage"].ToString();
                            obj.Passport = row["Passport"].ToString();
                            obj.PassportImage = row["PassportImage"].ToString();
                            obj.PanCardNo = row["PanCardNo"].ToString();
                            obj.PanCardImage = row["PanCardImage"].ToString();
                            obj.PinNumber = row["PinNumber"].ToString();
                            obj.BankImage = row["BankImage"].ToString();
                            obj.Level = row["Level"] == DBNull.Value ? 0 : Convert.ToInt32(row["Level"]);
                            obj.PinId = row["PinId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinId"]);
                            obj.PlanId = row["PlanId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PlanId"]);
                            obj.IsActive = row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["IsActive"]);
                            obj.CityName = row["CityName"].ToString();
                            obj.StateName = row["StateName"].ToString();
                            obj.CategoryName = row["MainMenuName"].ToString();
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
        public IList<UserDto> GetAllByCategory()
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();
            var Params = Param(_user, 19);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            obj.Code = row["Code"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.GuardianName = row["GuardianName"].ToString();
                            obj.Age = row["Age"] == DBNull.Value ? 0 : Convert.ToInt32(row["Age"]);
                            obj.Birthday = row["Birthday"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["Birthday"]);
                            obj.Gender = row["Gender"].ToString();
                            obj.MaritalStatus = row["MaritalStatus"].ToString();
                            obj.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                            obj.Address = row["Address"].ToString();
                            obj.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            obj.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            obj.ZipCode = row["ZipCode"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.EmailId = row["EmailId"].ToString();
                            obj.AccountNo = row["AccountNo"].ToString();
                            obj.Ifscode = row["Ifscode"].ToString();
                            obj.BankAddress = row["BankAddress"].ToString();
                            obj.BankId = row["BankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BankId"]);
                            obj.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);
                            obj.NomineeName = row["NomineeName"].ToString();
                            obj.NomineeRelationId = row["NomineeRelationId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeRelationId"]);
                            obj.NomineeZipCode = row["NomineeZipCode"].ToString();
                            obj.NomineeAddress = row["NomineeAddress"].ToString();
                            obj.NomineeAge = row["NomineeAge"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeAge"]);
                            obj.NomineeCityId = row["NomineeCityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeCityId"]);
                            obj.IntroducerCode = row["IntroducerCode"].ToString();
                            obj.IntroducerId = row["IntroducerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["IntroducerId"]);
                            obj.UplineCode = row["UplineCode"].ToString();
                            obj.UplineID = row["UplineID"] == DBNull.Value ? 0 : Convert.ToInt32(row["UplineID"]);
                            obj.Password = row["Password"].ToString();
                            obj.IsVerify = row["IsVerify"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVerify"]);
                            obj.VerifyDate = row["VerifyDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["VerifyDate"]);
                            obj.VerifyBy = row["VerifyBy"] == DBNull.Value ? 0 : Convert.ToInt64(row["VerifyBy"]);
                            obj.PaymentReceive = row["PaymentReceive"].ToString();
                            obj.ProductReceive = row["ProductReceive"].ToString();
                            obj.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            obj.Position = row["Position"].ToString();
                            obj.UserTypeId = row["UserTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserTypeId"]);
                            obj.RankId = row["RankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RankId"]);
                            obj.Profile = row["Profile"].ToString();
                            obj.Aadhar = row["Aadhar"].ToString();
                            obj.AadharImage = row["AadharImage"].ToString();
                            obj.VoterId = row["VoterId"].ToString();
                            obj.VoterIdImage = row["VoterIdImage"].ToString();
                            obj.DlNo = row["DlNo"].ToString();
                            obj.DlNoImage = row["DlNoImage"].ToString();
                            obj.Passport = row["Passport"].ToString();
                            obj.PassportImage = row["PassportImage"].ToString();
                            obj.PanCardNo = row["PanCardNo"].ToString();
                            obj.PanCardImage = row["PanCardImage"].ToString();
                            obj.PinNumber = row["PinNumber"].ToString();
                            obj.BankImage = row["BankImage"].ToString();
                            obj.Level = row["Level"] == DBNull.Value ? 0 : Convert.ToInt32(row["Level"]);
                            obj.PinId = row["PinId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinId"]);
                            obj.PlanId = row["PlanId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PlanId"]);
                            obj.IsActive = row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["IsActive"]);
                            obj.CityName = row["CityName"].ToString();
                            obj.StateName = row["StateName"].ToString();
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
        public IList<CurrencyMasterDto> GetAllCurrencyDetails()
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();

            var Params = Param(_user, 6);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CurrencyMasterDto> results = new List<CurrencyMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            CurrencyMasterDto obj = new CurrencyMasterDto();

                            obj.CurrencyName = row["CurrencyName"].ToString();
                            obj.CurrencyPrice = row["CurrencyPrice"].ToString();
                            obj.Date = row["Date"].ToDataConvertNullDateTime();

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
        public IList<CurrencyMasterForExcel> GetAllCurrencyDetailsForExcel()
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();

            var Params = Param(_user, 6);
            if (Params != null)
            {
                try
                { 
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CurrencyMasterForExcel> results = new List<CurrencyMasterForExcel>();

                        foreach (DataRow row in rows)
                        {
                            CurrencyMasterForExcel obj = new CurrencyMasterForExcel();

                            obj.Mul_Divide = row["Mul_Divide"].ToString();
                            obj.Currency = row["CurrencyName"].ToString();
                            obj.Value = row["CurrencyPrice"].ToDataConvertDouble();
                            obj.Date = row["Date"].ToDataConvertNullDateTime();

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
        public IList<CurrencyMasterForExcel> GetAllParentCurrencyDetails()
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();

            var Params = Param(_user, 7);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CurrencyMasterForExcel> results = new List<CurrencyMasterForExcel>();

                        foreach (DataRow row in rows)
                        {
                            CurrencyMasterForExcel obj = new CurrencyMasterForExcel();

                            obj.Mul_Divide = row["Mul_Divide"].ToString();
                            obj.Currency = row["Currency"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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
        public IList<CurrencyMasterForExcel> GetAllParentCurrencyDetailsForList()
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();

            var Params = Param(_user, 8);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CurrencyMasterForExcel> results = new List<CurrencyMasterForExcel>();

                        foreach (DataRow row in rows)
                        {
                            CurrencyMasterForExcel obj = new CurrencyMasterForExcel();

                            obj.Mul_Divide = row["Mul_Divide"].ToString();
                            obj.Currency = row["Currency"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();
                            obj.Date = row["Start_Date"].ToDataConvertNullDateTime();
                            obj.ConvertedCurrency = row["ConvertedCurrency"].ToString();

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
        public DataSet getExchangeCurrencyDetails()
        {
            UserDto obj = new UserDto();
           
            var Params = Param(obj, 9);

            if (Params != null && Params.Count() > 0)
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;
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
        public IList<UserDto> getbyUserTypeID(int? UserTypeID,long? ID)
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();
            _user.UserTypeId = UserTypeID;
            _user.Id = ID;
            var Params = Param(_user, 19);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.Id = row["Id"].ToDataConvertInt64();
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
        public IList<CompanyDivisionDto> GetCompanyDivision()
        {
            DataSet _ds = null;
            UserDto data = new UserDto();
            var Params = Param(data, 10);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CompanyDivisionDto> results = new List<CompanyDivisionDto>();

                        foreach (DataRow row in rows)
                        {
                            CompanyDivisionDto obj = new CompanyDivisionDto();

                            obj.CompanyDivisionId = row["CompanyDivisionId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CompanyDivisionId"]);
                            obj.CompanyDivisionName = row["CompanyDivisionName"].ToString();
                            obj.CompanyDivisionIsActive = row["CompanyDivisionIsActive"] == DBNull.Value ? true : Convert.ToBoolean(row["CompanyDivisionIsActive"]);

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
        public IList<AccManageDto> GetAccManage()
        {
            DataSet _ds = null;
            UserDto data = new UserDto();
            var Params = Param(data, 11);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<AccManageDto> results = new List<AccManageDto>();

                        foreach (DataRow row in rows)
                        {
                            AccManageDto obj = new AccManageDto();
                            obj.IAM_Id = row["IAM_Id"] == DBNull.Value ? 0 : Convert.ToInt32(row["IAM_Id"]);
                            obj.IAM_CWPeriod = row["IAM_CWPeriod"] == DBNull.Value ? 0 : Convert.ToInt32(row["IAM_CWPeriod"]);
                            obj.IAM_Year = row["IAM_Year"] == DBNull.Value ? 0 : Convert.ToInt32(row["IAM_Year"]);
                            obj.IAM_SelfPeriod = row["IAM_SelfPeriod"].ToString();
                            obj.IAM_StartDate = row["IAM_StartDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["IAM_StartDate"]);
                            obj.IAM_EndDate = row["IAM_EndDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["IAM_EndDate"]);

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

        public IList<UserDto> getAllIncentiveUser()
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();
            
            var Params = Param(_user, 20);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            //obj.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(row["Id"]);
                            //obj.Code = row["Code"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.GuardianName = row["GuardianName"].ToString();
                            //obj.Age = row["Age"] == DBNull.Value ? 0 : Convert.ToInt32(row["Age"]);
                            //obj.Birthday = row["Birthday"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["Birthday"]);
                            //obj.Gender = row["Gender"].ToString();
                            //obj.MaritalStatus = row["MaritalStatus"].ToString();
                            //obj.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                            //obj.Address = row["Address"].ToString();
                            //obj.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            //obj.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            //obj.ZipCode = row["ZipCode"].ToString();
                            //obj.MobileNo = row["MobileNo"].ToString();
                            //obj.EmailId = row["EmailId"].ToString();
                            //obj.AccountNo = row["AccountNo"].ToString();
                            //obj.Ifscode = row["Ifscode"].ToString();
                            //obj.BankAddress = row["BankAddress"].ToString();
                            //obj.BankId = row["BankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BankId"]);
                            //obj.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);
                            //obj.NomineeName = row["NomineeName"].ToString();
                            //obj.NomineeRelationId = row["NomineeRelationId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeRelationId"]);
                            //obj.NomineeZipCode = row["NomineeZipCode"].ToString();
                            //obj.NomineeAddress = row["NomineeAddress"].ToString();
                            //obj.NomineeAge = row["NomineeAge"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeAge"]);
                            //obj.NomineeCityId = row["NomineeCityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["NomineeCityId"]);
                            //obj.IntroducerCode = row["IntroducerCode"].ToString();
                            //obj.IntroducerId = row["IntroducerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["IntroducerId"]);
                            //obj.UplineCode = row["UplineCode"].ToString();
                            //obj.UplineID = row["UplineID"] == DBNull.Value ? 0 : Convert.ToInt32(row["UplineID"]);
                            //obj.Password = row["Password"].ToString();
                            //obj.IsVerify = row["IsVerify"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVerify"]);
                            //obj.VerifyDate = row["VerifyDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["VerifyDate"]);
                            //obj.VerifyBy = row["VerifyBy"] == DBNull.Value ? 0 : Convert.ToInt64(row["VerifyBy"]);
                            //obj.PaymentReceive = row["PaymentReceive"].ToString();
                            //obj.ProductReceive = row["ProductReceive"].ToString();
                            //obj.ProductReceiveDate = row["ProductReceiveDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["ProductReceiveDate"]);
                            //obj.Position = row["Position"].ToString();
                            //obj.UserTypeId = row["UserTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserTypeId"]);
                            //obj.RankId = row["RankId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RankId"]);
                            //obj.Profile = row["Profile"].ToString();
                            //obj.Aadhar = row["Aadhar"].ToString();
                            //obj.AadharImage = row["AadharImage"].ToString();
                            //obj.VoterId = row["VoterId"].ToString();
                            //obj.VoterIdImage = row["VoterIdImage"].ToString();
                            //obj.DlNo = row["DlNo"].ToString();
                            //obj.DlNoImage = row["DlNoImage"].ToString();
                            //obj.Passport = row["Passport"].ToString();
                            //obj.PassportImage = row["PassportImage"].ToString();
                            //obj.PanCardNo = row["PanCardNo"].ToString();
                            //obj.PanCardImage = row["PanCardImage"].ToString();
                            //obj.PinNumber = row["PinNumber"].ToString();
                            //obj.BankImage = row["BankImage"].ToString();
                            //obj.Level = row["Level"] == DBNull.Value ? 0 : Convert.ToInt32(row["Level"]);
                            //obj.PinId = row["PinId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinId"]);
                            //obj.PlanId = row["PlanId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PlanId"]);
                            //obj.IsActive = row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["IsActive"]);

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
        public IList<UserDto> getAllRptMgr()
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();

            var Params = Param(_user, 12);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                           
                            obj.Id = row["ID"].ToDataConvertInt64();
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

        public UserDto GetApproverID(string UserTypeId)
        {
            DataSet _ds = null;
            UserDto _user = new UserDto();
            _user.UserTypeId = UserTypeId.ToConvertNullInt();
            var Params = Param(_user, 22);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);
                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        UserDto result = new UserDto();
                        {
                            result.ApproverMailID = row["ApproverMailID"].ToString();

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

        
        //**User Creation with approval
        public UserDto UserApproved(UserDto obj)
        {
            DataSet _ds = null;
            var Params = Param(obj, 23);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        UserDto result = new UserDto();
                        {
                            result.GuardianName = row["RequestStatus"].ToString();
                            result.Id = row["ID"].ToDataConvertInt64();
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



        public UserDto GetByIdforUserApproval(long id)
        {
            DataSet _ds = null;

            UserDto _user = new UserDto();
            _user.Id = id;
            var Params = Param(_user, 24);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        UserDto result = new UserDto();
                        {
                           
                            result.Code = row["Code"].ToString();
                            result.Password = row["Password"].ToString();
                            result.Name = row["Name"].ToString();
                            result.EmailId = row["EmailId"].ToString();
                           
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

        public UserDto getUserIDAndPassword(UserDto obj)
        {
            DataSet _ds = null;
            var Params = Param(obj, 25);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        UserDto result = new UserDto();
                        {
                            result.Code = row["Code"].ToString();
                            result.Password = row["Password"].ToString();
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

        //Check Email id duplication 

        public bool ChkMailID(UserDto obj)
        {
            var Params = Param(obj, 26);
            if (Params != null)
            {
                try
                { 
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (i.ToDataConvertInt64() > 0)
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

        public bool checkEmailIdExistInPP(UserDto obj)
        {
            var Params = Param(obj, 27);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "UspMember", Params);

                    if (i.ToDataConvertInt64() > 0)
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
        
    }
}