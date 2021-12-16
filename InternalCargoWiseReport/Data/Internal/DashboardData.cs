using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InternalQuery;
using System.Linq;
using ICWR.QueryInternal;

namespace ICWR.Data
{
    public class DashboardData
    {
        //public DataTable GetAgeing(string DateFrom, string CompanyCode)
        //{
        //    DataTable dt = Query.AgeingQuery(DateFrom, CompanyCode);
        //    return dt;
        //}

        public DataTable GetCountUserLogined(bool isLogin)
        {
            OutResult or = QueryForDashboard.GetCountUserActiveAccCountryCode(isLogin);
            return or.dt;
        }

        public DataTable GetUserLoginedDetail(string CompanyCode, bool isLogin)
        {
            OutResult or = QueryForDashboard.GetUserActiveAccCountryCode(CompanyCode, isLogin);
            return or.dt;
        }

        public DataTable GetAllDashBoard()
        {
            if (LovelySession.Lovely != null)
            {
                if (LovelySession.Lovely.PermissionsCompany != null)
                {
                    OutResult or = QueryForDashboard.GetDashboardCount();
                    if (or.dt != null && or.dt.Rows.Count > 0)
                    {
                        var distinctNames = (from row in or.dt.AsEnumerable()
                                             select row.Field<string>("GC_Code")).Distinct();

                        if (distinctNames != null)
                        {
                            var uniqueCategories = LovelySession.Lovely.PermissionsCompany.Select(p => p.CompanyCode).Distinct();

                            var uniqueListing = distinctNames.Except(uniqueCategories);

                            int second = uniqueListing.Count();
                            if (second > 0)
                            {
                                for (int i = second - 1; i >= 0; i--)
                                {
                                    string ss = uniqueListing.ElementAt(i).ToString();

                                    DataRow[] rows = or.dt.Select("GC_Code = '" + uniqueListing.ElementAt(i).ToString() + "'");  //'UserName' is ColumnName

                                    foreach (DataRow row in rows)
                                        or.dt.Rows.Remove(row);
                                }
                                or.dt.AcceptChanges();
                            }
                        }
                    }
                    return or.dt;
                }
            }
            return null;
        }

        public DataSet GetDashBoardByFlag(string CompanyCode, string BranchCode, int flag)
        {
            InnerOutResult or = QueryInternal.QueryInternal.GetDashboardJobNotOpened(CompanyCode, BranchCode, flag);
            return or.ds;
        }

        //public DataTable GetDashBoardByFlag(string CompanyCode, string BranchCode, int flag)
        //{
        //    OutResult or = QueryForDashboard.GetDashboardJobNotOpened(CompanyCode, BranchCode, flag);
        //    return or.dt;
        //}
    }
}