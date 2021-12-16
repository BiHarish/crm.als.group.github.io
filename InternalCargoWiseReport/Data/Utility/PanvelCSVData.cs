using ICWR.Data;
using ICWR.Data.Utility;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalCargoWiseReport.Data.Utility
{
    [Serializable]
    public class CSVOur
    {
        public List<CSVTarriff> tarrif { get; set; }
        public List<CSVCustomer> customer { get; set; }
    }
    [Serializable] 
    public class CSVTarriff
    {
        public string Name { get; set; }
        public string Code { get; set; }

    }
    [Serializable]
    public class CSVCustomer
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string GSTIN { get; set; }
    }



    public class OrgMasterDto
    {
        public string OrgName { get; set; }
        public string RegistrationNumber { get; set; }
    }
    public class PanvelCSVData 
    {
        //public CSVOur GetData()
        //{
        //    try
        //    {
        //        XMLPanvelData panvelData = new XMLPanvelData();
        //        DataSet _ds = panvelData.GetTariffCustomerCode("REV");
        //        CSVOur cc = new CSVOur();
        //        if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        //        {
        //            DataRowCollection rows = _ds.Tables[0].Rows;
        //            List<CSVTarriff> results = new List<CSVTarriff>();

        //            foreach (DataRow row in rows)
        //            {
        //                CSVTarriff obj = new CSVTarriff();

        //                obj.Name = row["AC_Desc"].ToString();
        //                obj.Code = row["AC_Code"].ToString();

        //                results.Add(obj);
        //            }
        //            cc.tarrif = results;
        //        }
        //        if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[1].Rows.Count > 0)
        //        {
        //            DataRowCollection rows = _ds.Tables[1].Rows;
        //            List<CSVCustomer> results = new List<CSVCustomer>();

        //            foreach (DataRow row in rows)
        //            {
        //                CSVCustomer obj = new CSVCustomer();

        //                obj.Name = row["OH_FullName"].ToString();
        //                obj.Code = row["OH_Code"].ToString();
        //                obj.GSTIN = row["Ok_CustomsRegNo"].ToString();

        //                results.Add(obj);
        //            }
        //            cc.customer = results;

        //            return cc;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {

        //    }
        //    return null;
        //}

        SqlParameter[] Param(OrgMasterDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@OrgName", SqlDbType.NVarChar);
            Param[0].Value = obj.OrgName;

            Param[1] = new SqlParameter("@RegistrationNumber", SqlDbType.NVarChar);
            Param[1].Value = obj.RegistrationNumber;

            Param[2] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[2].Value = flag;



            return Param;
        }
        public CSVOur GetData()
        {
            try
            {
                OrgMasterDto request = new OrgMasterDto();

                var Params = Param(request, 1);

                DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.Csvconnection, CommandType.StoredProcedure, "uspOrgMaster", Params);
                CSVOur cc = new CSVOur();
                if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    DataRowCollection rows = _ds.Tables[0].Rows;
                    List<CSVTarriff> results = new List<CSVTarriff>();

                    foreach (DataRow row in rows)
                    {
                        CSVTarriff obj = new CSVTarriff();

                        obj.Name = row["AC_Desc"].ToString();
                        obj.Code = row["AC_Code"].ToString();

                        results.Add(obj);
                    }
                    cc.tarrif = results;
                }
                if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[1].Rows.Count > 0)
                {
                    DataRowCollection rows = _ds.Tables[1].Rows;
                    List<CSVCustomer> results = new List<CSVCustomer>();

                    foreach (DataRow row in rows)
                    {
                        CSVCustomer obj = new CSVCustomer();

                        obj.Name = row["OH_FullName"].ToString();
                        obj.Code = row["OH_Code"].ToString();
                        obj.GSTIN = row["Ok_CustomsRegNo"].ToString();

                        results.Add(obj);
                    }
                    cc.customer = results;

                    return cc;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return null;
        }
    }
}