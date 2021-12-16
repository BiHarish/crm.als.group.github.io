using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class SalesRepMasterData
    {
        SqlParameter[] Param(SalesRepMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[12];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = obj.Name;

            Param[2] = new SqlParameter("@MailID", SqlDbType.NVarChar);
            Param[2].Value = obj.MailID;

            Param[3] = new SqlParameter("@Department", SqlDbType.NVarChar);
            Param[3].Value = obj.Department;

            Param[4] = new SqlParameter("@Region", SqlDbType.NVarChar);
            Param[4].Value = obj.Region;


            Param[5] = new SqlParameter("@Status", SqlDbType.NVarChar);
            Param[5].Value = obj.Status;

            Param[6] = new SqlParameter("@Designation", SqlDbType.NVarChar);
            Param[6].Value = obj.Designation;

            Param[7] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[7].Value = obj.CreateBy;

            Param[8] = new SqlParameter("@CreateOn", SqlDbType.DateTime);
            Param[8].Value = obj.CreateOn;

            Param[9] = new SqlParameter("@ModifyBy", SqlDbType.BigInt);
            Param[9].Value = obj.ModifyBy;

            Param[10] = new SqlParameter("@ModifyOn", SqlDbType.DateTime);
            Param[10].Value = obj.ModifyOn;

            Param[11] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[11].Value = flag;

            return Param;
        }
      
        public IList<SalesRepMasterDto> GetAll()
        {
            DataSet _ds = null;
            SalesRepMasterDto _salesRepMasterData = new SalesRepMasterDto();
           
            var Params = Param(_salesRepMasterData, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSalesRepMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<SalesRepMasterDto> results = new List<SalesRepMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            SalesRepMasterDto obj = new SalesRepMasterDto();

                            obj.ID=row["ID"].ToDataConvertInt64();
                            obj.Name=row["Name"].ToString();
                            obj.MailID=row["MailID"].ToString();
                            obj.Department=row["Department"].ToString();
                            obj.Region=row["Region"].ToString();
                            obj.Status=row["Status"].ToDataConvertBool();
                            obj.Designation=row["Designation"].ToString();
                            obj.CreateBy=row["CreateBy"].ToDataConvertNullInt64();
                            obj.CreateOn=row["CreateOn"].ToDataConvertNullDateTime();
                            obj.ModifyBy=row["ModifyBy"].ToDataConvertNullInt64();
                            obj.ModifyOn = row["ModifyOn"].ToDataConvertNullDateTime();
                            obj.SalesCordinatorID = row["SalesCordinatorID"].ToDataConvertNullInt64();
                            obj.Type = row["Type"].ToString();

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

        public SalesRepMasterDto GetById(long id)
        {
            DataSet _ds = null;

            SalesRepMasterDto _salesRepMasterData = new SalesRepMasterDto();
            _salesRepMasterData.ID = id;
            var Params = Param(_salesRepMasterData, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSalesRepMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        SalesRepMasterDto obj = new SalesRepMasterDto();
                        {
                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Name = row["Name"].ToString();
                            obj.MailID = row["MailID"].ToString();
                            obj.Department = row["Department"].ToString();
                            obj.Region = row["Region"].ToString();
                            obj.Status = row["Status"].ToDataConvertBool();
                            obj.Designation = row["Designation"].ToString();
                            obj.InquiryNo = row["OrgInquiryID"].ToString();
                            obj.OrgName = row["OrgName"].ToString();
                            obj.CorMailID = row["CorMailID"].ToString();

                        }
                        return obj;
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