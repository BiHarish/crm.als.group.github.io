using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Sql;

namespace ICWR.Data 
{
    public class UploadOrgChargeData
    {
        public long uploadOrgData(UploadOrgDataDto obj)
        {
            SqlParameter[] Param = new SqlParameter[1];

            Param[0] = new SqlParameter("@dtOrgData", obj.dtOrgData);

            if (Param != null)
            {
                try
                {
                    Sql.LongReturn i = Sql.Easy<UploadOrgDataDto, object>.Insert(obj, "uspUploadOrgData", 1);

                    if (i.value >0)
                    {
                        return i.value;
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
        public long uploadChargeCodeData(UploadChargeCodeDto obj)
        {
            SqlParameter[] Param = new SqlParameter[1];

            Param[0] = new SqlParameter("@dtChargeCode", obj.dtChargeCode);

            if (Param != null)
            {
                try
                {
                    Sql.LongReturn i = Sql.Easy<UploadChargeCodeDto, object>.Insert(obj, "uspUploadChargeCodeData", 1);

                    if (i.value > 0)
                    {
                        return i.value;
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


    }
}