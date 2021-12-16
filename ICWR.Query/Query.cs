using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICWR.QueryInternal
{
    public class QueryInternal
    {
        private static string Get()
        {
            //return "Data Source=ap6ggn.wisegrid.net;database=OdysseyAP6GGN;user=EnterpriseDbuser_OdysseyAP6GGN_Debabrata.Banerjee;password=moondoo@321;";
            return "Data Source=ap6ggn.wisegrid.net;database=OdysseyAP6GGN;user=EnterpriseDbuser_OdysseyAP6GGN_AP6.Debabrata.Banerjee;password=moondoo@321;";
        }
        public static InnerOutResult GetResult(string query)
        {
            DataSet _ds = null;
            InnerOutResult or = new InnerOutResult();
            try
            {
                _ds = SqlHelper.ExecuteDataset(Get(), CommandType.Text, query);

                if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    or.dt = _ds.Tables[0];
                    or.ex = null;
                }
            }
            catch (Exception ex)
            {
                or.dt = null;
                or.ex = ex;
            }
            return or;
        }
        public static InnerOutResult GetResultInDataSet(string query)
        {
            DataSet _ds = null;
            InnerOutResult or = new InnerOutResult();
            try
            {
                _ds = SqlHelper.ExecuteDataset(Get(), CommandType.Text, query);

                if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 )
                {
                    or.ds = _ds;
                    or.ex = null;
                }
            }
            catch (Exception ex)
            {
                or.dt = null;
                or.ex = ex;
            }
            return or;
        }
        public static InnerOutResult PNLBHSQuery(string EndDate, string companyCode)
        {
            string companyGeneratedCode;
            if (companyCode.Equals("All"))
            {
                return null;
                //companyGeneratedCode = ")SELECT   [oh_code] FROM A WHERE [ah_OutstandingAmount] != 0 --and oh_code = 'FREREADAR'" + "  \n";
            }
            else
            {
                return null;
                //companyGeneratedCode = ")SELECT   [oh_code] FROM A WHERE [ah_OutstandingAmount] != 0 and oh_code = '" + partyCode + "'" + "  \n";
            }

            string query = "";


            return GetResult(query);
            //SELECT SUM(OutStandingTotal) FROM @OUTPUT  
        }
        public static InnerOutResult UserRDisptach(string datefrom, string dateto, string company)
        {
            string query =
                            "select  at.AH_TransactionNum As InvoiceNo,at.AH_ConsolidatedInvoiceRef As JobInvoiceNo,jh.JH_JobNum As ShipmentId," + " " +
                            "at.AH_TransactionType As JobType,jh.JH_A_JOP as JobOpenDate,at.AH_PostDate As PostDate,at.AH_DueDate As DueDate," + " " +
                            "at.AH_OSTotal As TransAmount,at.AH_FullyPaidDate As FullyPaidDate," + " " +
                            "dbo.GetCustomFieldByName(js.JS_PK,'Invoice Dispatched Date') As DispatchDate," + " " +
                            "gb.GB_BranchName as Branch,oh.OH_FullName as LocalClientName" + " " +
                            "from dbo.AccTransactionHeader at " + " " +
                            "Left Join JobHeader jh on at.AH_JH=jh.JH_PK" + " " +
                            "left Join GlbBranch gb on at.AH_GB=gb.GB_PK" + " " +
                            "Left Join OrgHeader oh on at.AH_OH=oh.OH_PK" + " " +
                            "Left Join JobShipment js on jh.JH_ParentID=js.JS_PK" + " " +
                            "WHere AH_Ledger='AR'  AND JH_GC = '" + company + "'" + "" +
                             "And jh.JH_A_JOP between '" + datefrom + "' and '" + dateto + "'" + " " +
                            "ORDER BY jh.JH_JobNum,at.AH_TransactionNum";

            return GetResult(query);
        }

        public static InnerOutResult MonthlyCostRevenue(string datefrom, string dateto, string company)
        {
            string query =

                    "SELECT  Jh_JobNum,SUM(JR.JR_LOCALSELLAMT)AS JOBREVENUE,Ah1.Ah_InvoiceDate,                                                                         " + "\n " +
                    " SUM(JR.JR_LOCALCOSTAMT)AS JOBCOST   INTO #TEMP                                                                                                    " + "\n " +
                    " FROM ACCTRANSACTIONHEADER AH1                                                                                                                     " + "\n " +
                    " LEFT JOIN ACCTRANSACTIONHEADER AH ON AH1.AH_PK=AH.AH_TRANSACTIONBELONGSTOGROUP                                                                    " + "\n " +
                    " LEFT JOIN JOBHEADER JH ON JH.JH_PK=AH1.AH_JH                                                                                                      " + "\n " +
                    " LEFT JOIN GLBBRANCH GB ON GB.GB_PK=JH.JH_GB                                                                                                       " + "\n " +
                    " LEFT JOIN GLBCOMPANY GC ON GC.GC_PK=JH.JH_GC                                                                                                      " + "\n " +
                    " INNER JOIN JOBCHARGE JR ON JR.JR_JH=JH.JH_PK                                                                                                      " + "\n " +
                    " WHERE  AH1.AH_LEDGER in ('AR','AP')  AND AH1.AH_INVOICEDATE between '" + datefrom + "' and '" + dateto + "'                                        " + "\n " +
                    "AND GC.GC_PK=  '" + company + "'                                                                                                                  " + "\n " +
                    "GROUP BY  AH1.AH_TRANSACTIONNUM ,Jh_JobNum,Ah1.Ah_InvoiceDate  order by  Ah_InvoiceDate                                                            " + "\n " +
                    "                                                                                                                                                   " + "\n " +
                    "                                                                                                                                                   " + "\n " +
                    "Select Row_Number() Over(order by Year(Ah_InvoiceDate))as [SRNo],                                                                                  " + "\n " +
                    "Concat(DateName(mm,DATEADD(mm,Month( Ah_InvoiceDate),-1)),'-',Year(Ah_InvoiceDate)) as [Month-Year]                                                " + "\n " +
                    ",Count(Jh_Jobnum) as JobCount,Sum(JOBREVENUE) as TotalRevenue ,Sum(JOBCOST) as TotalCost, (Sum(JOBREVENUE)-Sum(JOBCOST))as Profit                  " + "\n " +
                    " From #temp                                                                                                                                        " + "\n " +
                    " Group By  Concat(Month( Ah_InvoiceDate),'-',Year(Ah_InvoiceDate)),Month( Ah_InvoiceDate),Year(Ah_InvoiceDate)                                     " + "\n " +
                    "  order by  Year(Ah_InvoiceDate),Month( Ah_InvoiceDate)                                                                                            " + "\n " +
                    "DROP TABLE #TEMP                                                                                                                                   ";

            return GetResult(query);
        }
        public static InnerOutResult UserRDisptachCreditNote(string datefrom, string dateto, string company)
        {
            string query =


"SELECT ROW_NUMBER()OVER(ORDER BY AH1.AH_TRANSACTIONNUM) AS SNO ,SUM(JR.JR_LOCALSELLAMT)AS JOBREVENUE, AH1.AH_TRANSACTIONNUM AS INVTRANSNO,                          " + "\n " +
" SUM(JR.JR_LOCALCOSTAMT)AS JOBCOST                                                                                                                                  " + "\n " +
" INTO #TEMP                                                                                                                                                         " + "\n " +
" FROM ACCTRANSACTIONHEADER AH1                                                                                                                                      " + "\n " +
" LEFT JOIN ACCTRANSACTIONHEADER AH ON AH1.AH_PK=AH.AH_TRANSACTIONBELONGSTOGROUP                                                                                     " + "\n " +
" LEFT JOIN JOBHEADER JH ON JH.JH_PK=AH1.AH_JH                                                                                                                       " + "\n " +
" LEFT JOIN GLBBRANCH GB ON GB.GB_PK=JH.JH_GB                                                                                                                        " + "\n " +
" LEFT JOIN GLBCOMPANY GC ON GC.GC_PK=JH.JH_GC                                                                                                                       " + "\n " +
" INNER JOIN JOBCHARGE JR ON JR.JR_JH=JH.JH_PK                                                                                                                       " + "\n " +
" WHERE  AH.AH_LEDGER='AR'  AND AH.AH_INVOICEDATE between  '" + datefrom + "' and '" + dateto + "'                                                                           " + "\n " +
"AND GC.GC_PK =  '" + company + "'                                                                                                            " + "\n " +
"GROUP BY  AH1.AH_TRANSACTIONNUM                                                                                                                                     " + "\n " +
"                                                                                                                                                                    " + "\n " +
"SELECT                                                                                                                                                              " + "\n " +
"	AH.AH_TRANSACTIONNUM AS [TRANS NO FOR CRedIT NOTE]                                                                                                               " + "\n " +
"	,CONVERT(VARCHAR(50),AH.AH_INVOICEDATE,106) AS [CRedIT NOTE POST DATE]                                                                                           " + "\n " +
"	,AH.AH_OSTOTAL AS [CRedIT NOTE AMT]                                                                                                                              " + "\n " +
"	,AH.AH_EXCHANGERATE [Credit Ex Rate]                                                                                                                             " + "\n " +
"	,(AH.AH_OSTOTAL*AH.AH_EXCHANGERATE) AS [Credit LOCAL INV AMT]                                                                                                    " + "\n " +
"	,CONVERT(VARCHAR(50),AH.AH_INVOICEDATE,106) AS [CRD INV DATE]                                                                                                    " + "\n " +
"	,AH1.AH_TRANSACTIONNUM AS [INVOICE NO]                                                                                                                           " + "\n " +
"	,CONVERT(VARCHAR(50),AH1.AH_INVOICEDATE,106) AS [INV POST DATE]                                                                                                  " + "\n " +
"	,CONVERT(VARCHAR(50),AH1.AH_INVOICEDATE,106) AS [INVDATE]                                                                                                        " + "\n " +
"	,CONVERT(VARCHAR(50),AH1.AH_DUEDATE,106) AS [INVOICE DUE DATE]                                                                                                   " + "\n " +
"	,AH.AH_RX_NKTRANSACTIONCURRENCY AS [CURRENCYTYPE]                                                                                                                " + "\n " +
"	,AH1.AH_OSTOTAL AS [INVOICEAMT]                                                                                                                                  " + "\n " +
"	,AH1.AH_EXCHANGERATE [Ex Rate]                                                                                                                                   " + "\n " +
"	,(AH1.AH_OSTOTAL*AH1.AH_EXCHANGERATE) AS [LOCAL INV AMT]                                                                                                         " + "\n " +
"	,JH.JH_JOBNUM AS [JOB NUMBER]                                                                                                                                    " + "\n " +
"     ,CONVERT(VARCHAR(11),JH.JH_A_JOP,106) AS [JOB OPEN DATE]                                                                                                       " + "\n " +
"     ,GB.GB_BRANCHNAME AS [BRANCH NAME]                                                                                                                             " + "\n " +
"	 ,OH1.OH_FullName AS [CUSTOMER NAME]                                                                                                                             " + "\n " +
"	 ,OH.OH_FullName as [Local AR Party]                                                                                                                             " + "\n " +
"	 ,TM.JOBREVENUE                                                                                                                                                  " + "\n " +
"	 ,TM.JOBCOST                                                                                                                                                     " + "\n " +
"	 ,(ISNULL(TM.JOBREVENUE,0)-ISNULL(TM.JOBCOST,0)) AS [JOB PROFIT MARGIN]                                                                                          " + "\n " +
"	,ROUND(((CONVERT(DECIMAL(18,3),TM.JOBREVENUE) - CONVERT(DECIMAL(18,3),TM.JOBCOST))/                                                                              " + "\n " +
"	 CONVERT(DECIMAL(18,3),case when TM.JOBREVENUE=0 then 1 else TM.JOBREVENUE end))*100,2) AS [JOB PROFIT MARG(%)]                                                  " + "\n " +
"	 ,CONVERT(VARCHAR(11),AH1.AH_FULLYPAIDDATE,106) AS [CRD FULLY PAID DATE]                                                                                         " + "\n " +
"	                                                                                                                                                                 " + "\n " +
"FROM ACCTRANSACTIONHEADER AH1                                                                                                                                       " + "\n " +
"LEFT JOIN ACCTRANSACTIONHEADER AH ON AH1.AH_PK=AH.AH_TRANSACTIONBELONGSTOGROUP                                                                                      " + "\n " +
"LEFT JOIN JOBHEADER JH ON JH.JH_PK=AH1.AH_JH                                                                                                                        " + "\n " +
"LEFT JOIN GLBBRANCH GB ON GB.GB_PK=JH.JH_GB                                                                                                                         " + "\n " +
"LEFT JOIN GLBCOMPANY GC ON GC.GC_PK=JH.JH_GC                                                                                                                        " + "\n " +
"Left join OrgAddress OA on AH1.Ah_OA_InvoiceAddressOverride = OA.OA_Pk                                                                                              " + "\n " +
"Left join OrgHeader OH on OH.OH_Pk = OA.OA_OH                                                                                                                       " + "\n " +
"Left join OrgAddress OA1 on JH.JH_OA_LocalChargesAddr = OA1.OA_Pk                                                                                                   " + "\n " +
"Left join OrgHeader OH1 on OH1.OH_Pk = OA1.OA_OH                                                                                                                    " + "\n " +
"LEFT JOIN #TEMP TM ON TM.INVTRANSNO=AH1.AH_TRANSACTIONNUM                                                                                                           " + "\n " +
"                                                                                                                                                                    " + "\n " +
"WHERE   AH.AH_LEDGER='AR' AND AH.AH_INVOICEDATE between  '" + datefrom + "' and '" + dateto + "'                                                                 " + "\n " +
" AND GC.GC_PK =  '" + company + "'                                                                                                              " + "\n " +
"ORDER BY AH.AH_FULLYPAIDDATE,AH1.AH_TRANSACTIONNUM DESC                                                                                                             " + "\n " +
"                                                                                                                                                                    " + "\n " +
"DROP TABLE #TEMP                                                                                                                                                    ";


            return GetResult(query);
        }
        public static InnerOutResult ColorDashboard()
        {

            string query = "SELECT  JS_PK AS JOBSHIPMENTID,JS_UNIQUECONSIGNREF AS JOBSHIPMENTNO,JK_TRANSPORTMODE,DATEDIFF(DAY,JW_ETA,GETDATE()) AS DAYS," + " " +
              "(CASE WHEN JK_TRANSPORTMODE = 'AIR' THEN(CASE WHEN DATEDIFF(DAY, JW_ETA, GETDATE()) IN(0, 1) THEN 'GREEN'" + " " +
              "WHEN DATEDIFF(DAY, JW_ETA, GETDATE()) > 1 AND DATEDIFF(DAY, JW_ETA, GETDATE()) <= 2 THEN 'Yellow'" + " " +
               "WHEN DATEDIFF(DAY, JW_ETA, GETDATE()) > 2 AND JK_TRANSPORTMODE = 'AIR' THEN 'Red' END)" + " " +
               "WHEN JK_TRANSPORTMODE = 'SEA' THEN(CASE WHEN DATEDIFF(DAY, JW_ETA, GETDATE())IN(0, 1, 2) THEN 'GREEN' WHEN" + " " +
               "DATEDIFF(DAY, JW_ETA, GETDATE()) > 2 AND DATEDIFF(DAY, JW_ETA, GETDATE()) <= 3  THEN 'Yellow'" + " " +
               "WHEN DATEDIFF(DAY, JW_ETA, GETDATE()) > 3  THEN 'Red' END) ELSE NULL  END) AS COLORDIFF, JW_ETA, GC_NAME AS COMPANYNAME" + " " +
               ", GB_BRANCHNAME INTO #ATA" + " " +
               "FROM JOBCONSHIPLINK" + " " +
               "INNER JOIN JOBSHIPMENT ON JN_JS = JS_PK" + " " +
               "INNER JOIN JOBCONSOL ON  JN_JK = JK_PK" + " " +
               "INNER JOIN  JOBCONSOLTRANSPORT ON   JK_PK = JW_PARENTGUID" + " " +
               "INNER JOIN JOBHEADER ON JH_PARENTID = JS_PK" + " " +
               "INNER JOIN GLBCOMPANY GC ON GC.GC_PK = JH_GC" + " " +
               "INNER JOIN GLBBRANCH GB ON GB.GB_PK = JH_GB" + " " +
               "WHERE JK_ISCANCELLED = 0 AND JK_TRANSPORTMODE != '' AND JK_ISVALID = 1" + " " +
               "AND JW_ATA IS NULL AND JW_ETA IS NOT NULL AND JS_ISCANCELLED = 0 AND JS_ISVALID = 1 AND" + " " +
               "JK_TRANSPORTMODE IN('AIR', 'SEA')" + " " +

 "SELECT JS_PK AS JOBSHIPMENTID,JS_UNIQUECONSIGNREF AS JOBSHIPMENTNO,JK_TRANSPORTMODE,(DATEDIFF(DAY, JW_ETD, GETDATE()) + 1) AS DAYS," + " " +
"(CASE WHEN JK_TRANSPORTMODE = 'AIR' THEN(CASE WHEN(DATEDIFF(DAY, JW_ETD, GETDATE()) + 1) IN(0, 1) THEN 'GREEN'" + " " +
"WHEN(DATEDIFF(DAY, JW_ETD, GETDATE()) + 1) > 1 AND(DATEDIFF(DAY, JW_ETD, GETDATE()) + 1) <= 2 THEN 'YELLOW'" + " " +
"WHEN(DATEDIFF(DAY, JW_ETD, GETDATE()) + 1) > 2 AND JK_TRANSPORTMODE = 'AIR' THEN 'RED' END)" + " " +
"WHEN JK_TRANSPORTMODE = 'SEA' THEN(CASE WHEN(DATEDIFF(DAY, JW_ETD, GETDATE()) + 1)IN(0, 1, 2) THEN 'GREEN' WHEN" + " " +
"(DATEDIFF(DAY, JW_ETD, GETDATE()) + 1) > 2 AND(DATEDIFF(DAY, JW_ETD, GETDATE()) + 1) <= 3  THEN 'YELLOW'" + " " +
"WHEN(DATEDIFF(DAY, JW_ETD, GETDATE()) + 1) > 3  THEN 'RED' END) ELSE NULL  END) AS COLORDIFF, JW_ETD, JW_ETA, GC_NAME AS COMPANYNAME" + " " +
", GB_BRANCHNAME  INTO #ATD" + " " +
"FROM JOBCONSHIPLINK" + " " +
"INNER JOIN JOBSHIPMENT ON JN_JS = JS_PK" + " " +
"INNER JOIN JOBCONSOL ON  JN_JK = JK_PK" + " " +
"INNER JOIN  JOBCONSOLTRANSPORT ON   JK_PK = JW_PARENTGUID" + " " +
"INNER JOIN JOBHEADER ON JH_PARENTID = JS_PK" + " " +
"INNER JOIN GLBCOMPANY GC ON GC.GC_PK = JH_GC" + " " +
"INNER JOIN GLBBRANCH GB ON GB.GB_PK = JH_GB" + " " +
"WHERE JK_ISCANCELLED = 0 AND JK_TRANSPORTMODE != '' AND JK_ISVALID = 1" + " " +
"AND JW_ATD IS NULL AND JW_ETD IS NOT NULL AND JS_ISCANCELLED = 0 AND JS_ISVALID = 1" + " " +
"AND JK_TRANSPORTMODE IN('AIR', 'SEA')" + " " +

            //**Carrier Do Date"
            "SELECT JS_PK AS JOBSHIPMENTID," + " " +
"'DARKRED' AS COLORDIFF, datediff(day, JW_ATA, getdate()) as CarrierDayDiff into #CarrierDoDate" + " " +
"FROM JOBSHIPMENT" + " " +
"LEFT JOIN JOBCONSHIPLINK ON JN_JS = JS_PK" + " " +
"LEFT JOIN JOBCONSOL ON  JN_JK = JK_PK" + " " +
"INNER JOIN  JOBCONSOLTRANSPORT ON   JK_PK = JW_PARENTGUID" + " " +
"LEFT JOIN GENCUSTOMADDONVALUE ON XV_PARENTID = JS_PK AND XV_NAME = 'CARRIER DO DATE'" + " " +
"INNER JOIN JOBHEADER ON JH_PARENTID = JS_PK" + " " +
"WHERE JS_ISCANCELLED = 0 AND JS_ISVALID = 1" + " " +
"AND SUBSTRING(JS_UNIQUECONSIGNREF,9,1)= 'I' AND XV_NAME IS NULL" + " " +
"AND JW_ATA IS NOT NULL AND JK_TRANSPORTMODE IN('AIR','SEA')" + " " +

//**Carrier Bl Not Release Date
"SELECT JS_PK AS JOBSHIPMENTID, JW_ATD, JK_TRANSPORTMODE,(DATEDIFF(DAY, JW_ATD, GETDATE() + 1)) AS CARRIERBLNOTRELEASEDAYS," + " " +
"(CASE WHEN JK_TRANSPORTMODE = 'AIR' THEN(CASE WHEN(DATEDIFF(DAY, JW_ATD, GETDATE() + 1)) IN(0, 1) THEN 'GREEN'" + " " +
"WHEN(DATEDIFF(DAY, JW_ATD, GETDATE() + 1)) > 1 AND(DATEDIFF(DAY, JW_ATD, GETDATE() + 1)) <= 2 THEN 'YELLOW'" + " " +
"WHEN(DATEDIFF(DAY, JW_ATD, GETDATE() + 1)) > 2 AND JK_TRANSPORTMODE = 'AIR' THEN 'DARKRED' END)" + " " +
"WHEN JK_TRANSPORTMODE = 'SEA' THEN(CASE WHEN(DATEDIFF(DAY, JW_ATD, GETDATE() + 1))IN(0, 1, 2) THEN 'GREEN' WHEN" + " " +
"(DATEDIFF(DAY, JW_ATD, GETDATE() + 1)) > 2 AND(DATEDIFF(DAY, JW_ATD, GETDATE() + 1)) <= 3  THEN 'YELLOW'" + " " +
"WHEN(DATEDIFF(DAY, JW_ATD, GETDATE() + 1)) > 3  THEN 'DARKRED' END) ELSE NULL  END) AS COLORDIFF" + " " +
"into #CarrierBlNotRelease" + " " +
" FROM JOBCONSHIPLINK" + " " +
"INNER JOIN JOBSHIPMENT ON JN_JS = JS_PK" + " " +
"LEFT JOIN GENCUSTOMADDONVALUE ON XV_PARENTID = JS_PK AND XV_NAME = 'CARRIER BL DATE'" + " " +
"INNER JOIN JOBCONSOL ON  JN_JK = JK_PK" + " " +
"INNER JOIN  JOBCONSOLTRANSPORT ON   JK_PK = JW_PARENTGUID" + " " +
"INNER JOIN JOBHEADER ON JH_PARENTID = JS_PK" + " " +
"WHERE JS_ISCANCELLED = 0 AND JS_ISVALID = 1 AND JK_ISCANCELLED = 0 AND JK_ISVALID = 1" + " " +
"AND JK_MASTERBILLISSUEDATE IS NULL AND JS_TRANSPORTMODE IN('SEA', 'AIR') AND SUBSTRING(JS_UNIQUECONSIGNREF,9,1)= 'I' AND" + " " +
" JW_ATD IS NOT NULL AND   XV_DATA IS NULL" + " " +


"SELECT Row_Number()over(order by AA.JOBSHIPMENTID) as Sno, AA.JOBSHIPMENTID,AA.JOBSHIPMENTNO,AA.JK_TRANSPORTMODE,AA.DAYS as EtaDays ," + " " +
"AD.DAYS as EtdDays,AA.COLORDIFF AS COLORETA,AD.COLORDIFF AS COLORETD," + " " +
"CONVERT(VARCHAR(11), AA.JW_ETA, 106) AS ETA," + " " +
"CONVERT(VARCHAR(11), AD.JW_ETD, 106) AS ETD, AA.COMPANYNAME,AA.GB_BRANCHNAME AS BRANCHNAME,CDD.COLORDIFF AS CARRIERDODATECOLOR," + " " +
"cdd.CarrierDayDiff, CBNRD.COLORDIFF AS CBNRDCOLORDIFF,CBNRD.CARRIERBLNOTRELEASEDAYS" + " " +
"FROM #ATA AA" + " " +
"LEFT JOIN #ATD AD ON AA.JOBSHIPMENTID=AD.JOBSHIPMENTID" + " " +
"LEFT JOIN #CARRIERDODATE CDD ON CDD.JOBSHIPMENTID=AA.JOBSHIPMENTID" + " " +
"LEFT JOIN #CarrierBlNotRelease CBNRD ON CBNRD.JOBSHIPMENTID=AA.JOBSHIPMENTID" + " " +

 "DROP TABLE #ATA,#ATD,#CarrierDoDate,#CarrierBlNotRelease";

            return GetResult(query);
        }

        public static InnerOutResult GetDashboardJobNotOpened(string CompanyCode, string BranchCode, int flag)
        {
            string query;
            switch (flag)
            {
                case (int)DashboardFetchType.JobPendingForOpening:
                    query =
                    ";with a as(  " + "  \n" +
                "select* from JobHeader" + "  \n" +
                "where SUBSTRING(JH_JobNum, 1, 1) = 'Q' and Jh_IsValid = 1 and JH_TH_NKQuoteNumber = '' and JH_Status<>'CLS'" + "  \n" +
                "),b as (select TT_TransportMode, a.Jh_JobNum[QuotationNo], a.JH_GS_NKRepSales[SalesRep]," + "  \n" +
                "a.JH_GS_NKRepOps[OperationRep], a.Jh_GC, a.JH_GB, a.JH_A_JOP as JH_A_JOP, " + "  \n" +
                "(case when TT_TRANSPORTMODE IN('LSE', 'ULD') then(CASE WHEN a.JH_A_JOP IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, a.JH_A_JOP, GETDATE())) <= 7 THEN 'GREEN'" + "  \n" +
                "WHEN(DATEDIFF(DAY, a.JH_A_JOP, GETDATE())) > 7 AND(DATEDIFF(DAY, a.JH_A_JOP, GETDATE())) <= 15 THEN 'YELLOW'" + "  \n" +
                "WHEN(DATEDIFF(DAY, a.JH_A_JOP, GETDATE())) > 15  THEN 'RED' END) ELSE NULL END)" + "  \n" +
                "when TT_TRANSPORTMODE IN('Sea', 'LCL', 'FCL') then(CASE WHEN a.JH_A_JOP IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, a.JH_A_JOP, GETDATE())) < = 15 THEN 'GREEN'" + "  \n" +
                "WHEN(DATEDIFF(DAY, a.JH_A_JOP, GETDATE())) > 15 AND(DATEDIFF(DAY, a.JH_A_JOP, GETDATE())) <= 30 THEN 'YELLOW'" + "  \n" +
                "WHEN(DATEDIFF(DAY, a.JH_A_JOP, GETDATE())) > 30  THEN 'RED' else null END) ELSE NULL END) END) as STATUS," + "  \n" +
                "GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName]" + "  \n" +
                "from JobHeader Right Join a on a.JH_JobNum = JobHeader.JH_TH_NKQuoteNumber" + "  \n" +
                "left Join GlbStaff[GS1] on GS1.GS_Code = a.JH_GS_NKRepSales" + "  \n" +
                "left Join GlbStaff[GS2] on GS2.GS_Code = a.JH_GS_NKRepOps" + "  \n" +
                "inner join RateOneOffShipment TT on TT.TT_TH = a.Jh_ParentId" + "  \n" +
                "where JobHeader.JH_JobNum is null  " + "  \n" +
                        "and a.Jh_gc='" + CompanyCode + "' and a.Jh_GB='" + BranchCode + "'" + "  \n" +
                        ")" + "  \n" +
                    "select QuotationNo,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep,Convert(varchar(11),JH_A_JOP,106) as JobOpenDate,STATUS from b " + "  \n" +
                    "select '0-7 Days' as AirGreen,'8-15 Days' as AirYellow,'15+ Days' as AirRed,'' as AirDarkRed,'0-15 Days' as SeaGreen,'16-30 Days' as SeaYellow,'30+ Days' as SeaRed,'' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.ATDATANotUpdated:
                    query =
                            ";with a as(select  JS_UniqueConsignRef [ShipmentNumber],JH_GS_NKRepSales [SalesRep],JH_GS_NKRepOps [OperationRep],Jh_GC,JH_GB," + "  \n" +
                             "GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName]," + "  \n" +
                             "CASE WHEN JW_ATA IS NULL THEN(CASE WHEN (DATEDIFF(DAY, JW_ETA, GETDATE())) < =1 THEN 'GREEN'" + "  \n" +
                             "WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) > 1 AND(DATEDIFF(DAY, JW_ETA, GETDATE())) <= 2 THEN 'YELLOW'" + "  \n" +
                             "WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) > 2  THEN 'RED' END) ELSE NULL END AS COLORDIFFATA," + "  \n" +
                             "CASE WHEN JW_ATD IS NULL THEN(CASE WHEN (DATEDIFF(DAY, JW_ETD, GETDATE())) < =1 THEN 'GREEN'" + "  \n" +
                             "WHEN(DATEDIFF(DAY, JW_ETD, GETDATE())) > 1 AND(DATEDIFF(DAY, JW_ETD, GETDATE())) <= 2 THEN 'YELLOW'" + "  \n" +
                             "WHEN(DATEDIFF(DAY, JW_ETD, GETDATE())) > 2 THEN 'RED' END) ELSE NULL END AS COLORDIFFATD,JW_ATA,JW_ATD,JW_ETA,JW_ETD" + "  \n" +
                             "from JobConShipLink" + "  \n" +
                             "inner join JobShipment ON JN_JS = JS_PK" + "  \n" +
                             "inner join JobConsol ON  JN_JK = JK_PK" + "  \n" +
                             "inner join  JobConsolTransport ON   JK_PK = JW_ParentGUID" + "  \n" +
                             "inner join JobHeader on JH_ParentID = JS_PK" + "  \n" +
                             "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales" + "  \n" +
                             "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps" + "  \n" +
                             "where JK_IsCancelled = 0 and JK_TransportMode != '' and JK_IsValid = 1 and(JW_ATA is null or JW_ATD is null) and JS_IsCancelled = 0 and JS_IsValid = 1 " + "  \n" +
                             "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                              ")" + "  \n" +
                             "select ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep," + "  \n" +
                             "Convert(varchar(11),JW_ETA,106) as EtaDate,Convert(varchar(11),JW_ETD,106) as EtdDate,COLORDIFFATA as ATASTATUS,COLORDIFFATD as ATDSTATUS from a" + "  \n" +
                    "select '0-1 Days' as AirGreen,'1-2 Days' as AirYellow,'2+ Days' as AirRed,'' as AirDarkRed,'0-2 Days' as SeaGreen,'2-3 Days' as SeaYellow,'3+ Days' as SeaRed,'' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.BlNotCreated:
                    query =
                "   ;with a as (" + "  \n" +
                     "select case JS_TransportMode when 'SEA' then 'SEA Export' else 'AIR Export' end as TransType,JS_UniqueConsignRef[ShipmentNumber],JH_GS_NKRepSales[SalesRep]," + "  \n" +
                     "JH_GS_NKRepOps[OperationRep],Jh_GC,JH_GB,jw_Etd , " + "  \n" +
                     "(CASE WHEN JW_TransportMode = 'AIR' THEN(CASE WHEN(DATEDIFF(DAY, jw_ETD, GETDATE())) <= 2 THEN 'Red'" + "  \n" +
                     "WHEN(DATEDIFF(DAY, jw_ETD, GETDATE())) > 2  THEN 'DarkRed' END)" + "  \n" +
                     "WHEN JW_TransportMode = 'SEA' THEN(CASE WHEN(DATEDIFF(DAY, jw_ETD, GETDATE())) <= 2 THEN 'Red' WHEN" + "  \n" +
                     "(DATEDIFF(DAY, jw_ETD, GETDATE())) > 2   THEN 'DarkRed' END) ELSE NULL  END) AS COLORDIFF,GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName]" + "  \n" +
                     "from JobConShipLink" + "  \n" +
                     "inner join JobShipment ON JN_JS = JS_PK" + "  \n" +
                     "inner join JobConsol ON  JN_JK = JK_PK" + "  \n" +
                     "inner join JobConsolTransport ON   JK_PK = JW_ParentGUID" + "  \n" +
                     "inner join JobHeader on JH_ParentID = JS_PK" + "  \n" +
                     "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales" + "  \n" +
                     "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps" + "  \n" +
                     "where JS_IsCancelled = 0 and JS_IsValid = 1 and JK_IsCancelled = 0 and JK_IsValid = 1 and" + "  \n" +
                     "JK_MasterBillNum = '' and JS_TransportMode in ('SEA', 'AIR') and SUBSTRING(JS_UniqueConsignRef,9,1)= 'E'                                           " + "  \n" +
                "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                "	)                                                                                                                                                                                                                                           " + "  \n" +
                    "select TransType,ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep,Convert(varchar(11),jw_ETd,106) as ETDDATE,COLORDIFF as Status from a " + "  \n" +
                    "select '' as AirGreen,'' as AirYellow,'0-2 Days' as AirRed,'2+ Days' as AirDarkRed,'' as SeaGreen,'' as SeaYellow,'0-2 Days' as SeaRed,'2+ Days' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.BlNotReleased:
                    query =
                    "; with a as ( " + "  \n" +
                    "select case JS_TransportMode when 'SEA' then 'SEA Export' else 'AIR Export' end as TransType,JS_UniqueConsignRef[ShipmentNumber],JH_GS_NKRepSales[SalesRep], " + "  \n" +
                    "JH_GS_NKRepOps[OperationRep],Jh_GC,JH_GB,jw_ATD, " + "  \n" +
                    "(CASE WHEN JW_TransportMode = 'AIR' THEN(CASE WHEN(DATEDIFF(DAY, jw_ATD, GETDATE())) <= 2 THEN 'Red' " + "  \n" +
                    "WHEN(DATEDIFF(DAY, jw_ATD, GETDATE())) > 2  THEN 'DarkRed' END) " + "  \n" +
                    "WHEN JW_TransportMode = 'SEA' THEN(CASE WHEN(DATEDIFF(DAY, jw_ATD, GETDATE())) <= 2 THEN 'Red' WHEN " + "  \n" +
                    "(DATEDIFF(DAY, jw_ATD, GETDATE())) > 2   THEN 'DarkRed' END) ELSE NULL  END) AS COLORDIFF,GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName] from JobConShipLink " + "  \n" +
                    "inner join JobShipment ON JN_JS = JS_PK " + "  \n" +
                    "inner join JobConsol ON  JN_JK = JK_PK " + "  \n" +
                    "inner join JobConsolTransport ON   JK_PK = JW_ParentGUID " + "  \n" +
                    "inner join JobHeader on JH_ParentID = JS_PK " + "  \n" +
                    "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales" + "  \n" +
                    "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps" + "  \n" +
                    "where JS_IsCancelled = 0 and JS_IsValid = 1 and JK_IsCancelled = 0 and JK_IsValid = 1 " + "  \n" +
                    "and JK_MasterBillissueDate is null  and JS_TransportMode in ('SEA', 'AIR') and SUBSTRING(JS_UniqueConsignRef,9,1)= 'E' " + "  \n" +
                    "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                    "	)                                                                                                                                                                                                                                           " + "  \n" +
                    "select TransType,ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep,Convert(varchar(11),jw_ATD,106) as ATDDATE,ColorDiff  as Status from a " + "  \n" +
                     "select '' as AirGreen,'' as AirYellow,'0-2 Days' as AirRed,'2+ Days' as AirDarkRed,'' as SeaGreen,'' as SeaYellow,'0-2 Days' as SeaRed,'2+ Days' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.BlHouseMasterNotReleased:
                    query =
                    ";with a as(select  case JS_TransportMode when 'SEA' then 'SEA Export' else 'AIR Export' end as TransType,JS_UniqueConsignRef [ShipmentNumber] ,JH_GS_NKRepSales [SalesRep],JH_GS_NKRepOps [OperationRep],Jh_GC,JH_GB  from JobShipment     " + "  \n" +
                             "inner join JobHeader on JH_ParentID = JS_PK                                                         " + "  \n" +
                             "where JS_IsCancelled = 0 and JS_IsValid = 1 and JS_HouseBill != '' and JS_HouseBillIssueDate is null  and JS_TransportMode in ('SEA','AIR') and SUBSTRING(JS_UniqueConsignRef,9,1)='E'" + "  \n" +
                    "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                    ")" + "  \n" +
                    "select TransType,ShipmentNumber,SalesRep,OperationRep from a " + "  \n" +
                    "select '' as AirGreen,'' as AirYellow,'' as AirRed,'' as AirDarkRed,'' as SeaGreen,'' as SeaYellow,'' as SeaRed,'' as SeaDarkRed ";
                    break;

                case (int)DashboardFetchType.BlHouseMasterNotCreated:
                    query =
                    ";with a as(select  case JS_TransportMode when 'SEA' then 'SEA Export' else 'AIR Export' end as TransType,JS_UniqueConsignRef [ShipmentNumber] ,JH_GS_NKRepSales [SalesRep],JH_GS_NKRepOps [OperationRep],Jh_GC,JH_GB  from JobShipment" + "  \n" +
                             "inner join JobHeader on JH_ParentID = JS_PK  " + "  \n" +
                             "where JS_IsCancelled = 0 and JS_IsValid = 1 and JS_HouseBill != ''  and JS_TransportMode in ('SEA','AIR') and SUBSTRING(JS_UniqueConsignRef,9,1)='E'" + "  \n" +
                    "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                    ")" + "  \n" +
                    "select TransType,ShipmentNumber,SalesRep,OperationRep from a ";
                    break;

                case (int)DashboardFetchType.CostNotBooked:
                    query =
                    ";with a as(select  distinct JH_JobNum [ShipmentNumber]	,JH_GS_NKRepSales [SalesRep],JH_GS_NKRepOps [OperationRep],GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName], " + "  \n" +
                     "(case when jw_TransportMode= 'Air' then(CASE WHEN JW_ATA IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ATA, GETDATE())) < 5 THEN 'GREEN' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ATA, GETDATE())) > 5 AND(DATEDIFF(DAY, JW_ATA, GETDATE())) <= 10 THEN 'YELLOW' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ATA, GETDATE())) > 10 AND(DATEDIFF(DAY, JW_ATA, GETDATE())) <= 15 THEN 'RED' " + "  \n" +
                     "when(DATEDIFF(DAY, JW_ATA, GETDATE())) > 15 Then 'DarkRed' END) ELSE NULL END) " + "  \n" +
                     "when jw_TransportMode = 'Sea' then(CASE WHEN JW_ATA IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ATA, GETDATE())) < 7 THEN 'GREEN' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ATA, GETDATE())) > 7 AND(DATEDIFF(DAY, JW_ATA, GETDATE())) <= 15 THEN 'YELLOW' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ATA, GETDATE())) > 16 AND(DATEDIFF(DAY, JW_ATA, GETDATE())) <= 30 THEN 'RED'  " + "  \n" +
                     "else null END) ELSE NULL END) " + "  \n" +
                     "END) as AtaStatus, " + "  \n" +
                     "(case when jw_TransportMode= 'Air' then(CASE WHEN JW_ATD IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ATD, GETDATE())) < 5 THEN 'GREEN' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ATD, GETDATE())) > 5 AND(DATEDIFF(DAY, JW_ATD, GETDATE())) <= 10 THEN 'YELLOW' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ATD, GETDATE())) > 10 AND(DATEDIFF(DAY, JW_ATD, GETDATE())) <= 15 THEN 'RED' " + "  \n" +
                     "when(DATEDIFF(DAY, JW_ATD, GETDATE())) > 15 Then 'DarkRed' END) ELSE NULL END) " + "  \n" +
                     "when jw_TransportMode = 'Sea' then(CASE WHEN JW_ATD IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ATD, GETDATE())) < 7 THEN 'GREEN' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ATD, GETDATE())) > 7 AND(DATEDIFF(DAY, JW_ATD, GETDATE())) <= 15 THEN 'YELLOW' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ATD, GETDATE())) > 16 AND(DATEDIFF(DAY, JW_ATD, GETDATE())) <= 30 THEN 'RED'  " + "  \n" +
                     "else null END) ELSE NULL END) " + "  \n" +
                     "END) as AtDStatus, " + "  \n" +
                     "DATEDIFF(DAY, JW_ATD, GETDATE()) as dayDiff, " + "  \n" +


                     "JW_ATA,JW_ATD,JW_ETA,JW_ETD,jw_Transportmode " + "  \n" +
                     "from JOBCHARGE " + "  \n" +
                     "LEFT join AccTransactionLines[AP] on JR_AL_APLine = [AP].AL_PK " + "  \n" +
                     "inner join JOBHEADER on JH_PK = JR_JH " + "  \n" +
                     "left join jobshipment on js_pk = jh_parentID " + "  \n" +
                     "left join JobConShipLink on  JN_JS = JS_PK " + "  \n" +
                     "left join JobConsol ON  JN_JK = JK_PK " + "  \n" +
                     "left join  JobConsolTransport ON   JK_PK = JW_ParentGUID " + "  \n" +
                     "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales " + "  \n" +
                     "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps " + "  \n" +
                     "where[AP].AL_LineType <> 'CST' and JH_Isvalid = 1 and JH_JobNum like 'S%' " + "  \n" +
                        " and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                        ")" + "  \n" +
                    "select ShipmentNumber,(SalesRepName + ' (' + SalesRep + ')') as SalesRep,(OperationRepName + ' (' + OperationRep + ')') as OperationRep," +
                    "Convert(varchar(11),JW_ATA,106) as ATADate,ATAStatus,Convert(varchar(11),JW_ATD,106) as ATDDate,ATDStatus  from a" + "  \n" +
                    "select '0-5 Days' as AirGreen,'6-10 Days' as AirYellow,'11-15 Days' as AirRed,'15+ Days' as AirDarkRed,'0-7 Days' as SeaGreen,'8-15 Days' as SeaYellow,'16-30 Days' as SeaRed,'30+' as SeaDarkRed ";

                    break;
                case (int)DashboardFetchType.RevenueNotBooked:
                    query =
                    ";with a as(select  distinct JH_JobNum [ShipmentNumber]	,JH_GS_NKRepSales [SalesRep],JH_GS_NKRepOps [OperationRep],GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName], " + "  \n" +
                     "(case when jw_TransportMode= 'Air' then(CASE WHEN JW_ETA IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) <= 1  THEN 'Yellow' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) > 1 THEN 'Red' END) ELSE NULL END) " + "  \n" +
                     "when jw_TransportMode = 'Sea' then(CASE WHEN JW_ETA IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) <= 1 THEN 'Yellow' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) > 1 THEN 'Red' else null END) ELSE NULL END) END) as ETAStatus, " + "  \n" +
                     "(case when jw_TransportMode= 'Air' then(CASE WHEN JW_ETD IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ETD, GETDATE())) <= 1 THEN 'Yellow' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ETD, GETDATE())) > 1  THEN 'Red' END) ELSE NULL END) " + "  \n" +
                     "when jw_TransportMode = 'Sea' then(CASE WHEN JW_ETD IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ETD, GETDATE())) <= 1 THEN 'Yellow' " + "  \n" +
                     "WHEN(DATEDIFF(DAY, JW_ETD, GETDATE())) > 1 THEN 'Red'else null END) ELSE NULL END) " + "  \n" +
                     "END) as ETDStatus, " + "  \n" +
                     "DATEDIFF(DAY, JW_ETD, GETDATE()) as dayDiff, " + "  \n" +
                     "JW_ETA,JW_ETD,jw_Transportmode from JOBCHARGE " + "  \n" +
                     "LEFT join AccTransactionLines[AR] on JR_AL_ARLine = [AR].AL_PK " + "  \n" +
                     "inner join JOBHEADER on JH_PK = JR_JH " + "  \n" +
                     "left join jobshipment on js_pk = jh_parentID " + "  \n" +
                     "left join JobConShipLink on  JN_JS = JS_PK " + "  \n" +
                     "left join JobConsol ON  JN_JK = JK_PK " + "  \n" +
                     "left join  JobConsolTransport ON   JK_PK = JW_ParentGUID " + "  \n" +
                     "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales " + "  \n" +
                     "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps " + "  \n" +
                     "where[AR].AL_LineType <> 'REV' and JH_Isvalid = 1 and JH_JobNum like 'S%' " + "  \n" +
                        "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                        ")" + "  \n" +
                    "select ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep," + "  \n" +
                    "Convert(varchar(11), JW_ETA, 106) as ETADate,ETAStatus,Convert(varchar(11), JW_ETD, 106)  as ETDDate,ETDStatus  from a " + "  \n" +
                    "select '' as AirGreen,'0-1 Days' as AirYellow,'1+ Days' as AirRed,'' as AirDarkRed,'' as SeaGreen,'0-1 Days' as SeaYellow,'1+ Days' as SeaRed,'' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.Unbilled:
                    query =
                    ";with a as(select  distinct JH_JobNum [ShipmentNumber]	,JH_GS_NKRepSales [SalesRep],JH_GS_NKRepOps [OperationRep]	from JOBCHARGE " + "  \n" +
                        "LEFT join AccTransactionLines [AR] on JR_AL_ARLine = [AR].AL_PK " + "  \n" +
                        "inner join JOBHEADER on JH_PK = JR_JH" + "  \n" +
                        "where [AR].AL_LineType <> 'REV' and JH_Isvalid = 1 and JH_JobNum like 'S%'" + "  \n" +
                        "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                        ")" + "  \n" +
                    "select ShipmentNumber,SalesRep,OperationRep from a ";
                    break;
                case (int)DashboardFetchType.ShipmentPendingForClosure:
                    query =
                            "; WITH A AS(SELECT DISTINCT  JH_JOBNUM[SHIPMENTNUMBER], JH_GS_NKREPSALES[SALESREP], JH_GS_NKREPOPS[OPERATIONREP]," + "  \n" +
                            "GS1.GS_FULLNAME[SALESREPNAME], GS2.GS_FULLNAME[OPERATIONREPNAME], JH_A_JOP," + "  \n" +
                            "(CASE WHEN JS_TRANSPORTMODE = 'AIR' THEN(CASE WHEN JH.JH_A_JOP IS NOT NULL THEN(CASE WHEN(DATEDIFF(DAY, JH.JH_A_JOP, GETDATE())) <= 30 THEN 'GREEN'" + "  \n" +
                            "WHEN(DATEDIFF(DAY, JH.JH_A_JOP, GETDATE())) > 31 AND(DATEDIFF(DAY, JH.JH_A_JOP, GETDATE())) <= 45 THEN 'YELLOW'" + "  \n" +
                            "WHEN(DATEDIFF(DAY, JH.JH_A_JOP, GETDATE())) > 45  THEN 'RED' END) ELSE NULL END)" + "  \n" +
                            "WHEN JS_TRANSPORTMODE = 'SEA' THEN(CASE WHEN JH.JH_A_JOP IS NOT NULL THEN(CASE WHEN(DATEDIFF(DAY, JH.JH_A_JOP, GETDATE())) <= 30 THEN 'GREEN'" + "  \n" +
                            "WHEN(DATEDIFF(DAY, JH.JH_A_JOP, GETDATE())) > 31 AND(DATEDIFF(DAY, JH.JH_A_JOP, GETDATE())) <= 45 THEN 'YELLOW'" + "  \n" +
                            "WHEN(DATEDIFF(DAY, JH.JH_A_JOP, GETDATE())) > 45 THEN 'RED'  ELSE NULL END) ELSE NULL END) END) AS STATUS" + "  \n" +
                            "FROM JOBHEADER JH" + "  \n" +
                            "INNER JOIN JOBSHIPMENT JS ON JH.JH_PARENTID = JS.JS_PK--AND JH_PARENTTABLECODE = 'JS'" + "  \n" +
                            "LEFT JOIN GLBSTAFF[GS1] ON GS1.GS_CODE = JH.JH_GS_NKREPSALES" + "  \n" +
                            "LEFT JOIN GLBSTAFF[GS2] ON GS2.GS_CODE = JH.JH_GS_NKREPOPS " +"  \n" +
                            "where jh.JH_Status='CMP'  and jh.Jh_gc='" + CompanyCode + "' and jh.Jh_GB='" + BranchCode + "'" + "  \n" +
                            ")" + "  \n" +
                            "SELECT SHIPMENTNUMBER,(SALESREPNAME+' ('+SALESREP+')') AS SALESREP,(OPERATIONREPNAME+' ('+OPERATIONREP+')') AS OPERATIONREP,CONVERT(VARCHAR(11),JH_A_JOP,106) AS JOBOPENDATE,STATUS FROM A " + "  \n" +
                    "select '0-30 Days' as AirGreen,'31-45 Days' as AirYellow,'45+ Days' as AirRed,'' as AirDarkRed,'0-30 Days' as SeaGreen,'31-45 Days' as SeaYellow,'45+ Days' as SeaRed,'' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.DlNotCreated:
                    query =
                        ";with a as(  " + "  \n" +
                         "select  distinct JS_UniqueConsignRef  [ShipmentNumber],JH_GS_NKRepSales [SalesRep],JH_GS_NKRepOps [OperationRep],Jh_GC,JH_GB," + "  \n" +
                         "(CASE WHEN JW_TransportMode='AIR' THEN (CASE WHEN (DATEDIFF(DAY,JW_ATA,GETDATE())) <=2 THEN 'Red' " + "  \n" +
                         "WHEN (DATEDIFF(DAY,JW_ATA,GETDATE()))>2  THEN 'DarkRed' END) " + "  \n" +
                         "WHEN JW_TransportMode='SEA' THEN (CASE WHEN (DATEDIFF(DAY,JW_ATA,GETDATE()))<=2 THEN 'Red' WHEN" + "  \n" +
                         "(DATEDIFF(DAY,JW_ATA,GETDATE()))>2   THEN 'DarkRed' END) ELSE NULL  END) AS COLORDIFF," + "  \n" +
                         "jw_ATA,GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName] FROM JobShipment  " + "  \n" +
                         "left join GenCustomAddOnValue ON XV_ParentID = JS_PK and XV_Name = 'CARRIER DO DATE'" + "  \n" +
                         "inner join JobHeader on JH_ParentID = JS_PK  " + "  \n" +
                         "left  join JobConShipLink on  JN_JS = JS_PK " + "  \n" +
                         "left  join JobConsol ON  JN_JK = JK_PK  " + "  \n" +
                         "left  join  JobConsolTransport ON   JK_PK = JW_ParentGUID " + "  \n" +
                         "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales" + "  \n" +
                         "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps" + "  \n" +
                         "where JS_IsCancelled = 0 and JS_IsValid = 1 and SUBSTRING(JS_UniqueConsignRef,9,1)='I' and XV_Name is null  " + "  \n" +
                         "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                         ")" + "  \n" +
                         "select ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep,Convert(varchar(11),jw_ATA,106) as ATADATE,ColorDiff as Status from a  " + "  \n" +
                    "select '' as AirGreen,'' as AirYellow,'0-2 Days' as AirRed,'2+ Days' as AirDarkRed,'' as SeaGreen,'' as SeaYellow,'0-2 Days' as SeaRed,'2+ Days' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.DlNotReleased:
                    query =
                        ";with a as(  " + "  \n" +
                         "select distinct JS_UniqueConsignRef[ShipmentNumber],JH_GS_NKRepSales[SalesRep],JH_GS_NKRepOps[OperationRep],Jh_GC,JH_GB," + "  \n" +
                         "XV_Data as CarrierDODate," + "  \n" +
                         "(CASE WHEN JW_TransportMode = 'AIR' THEN(CASE WHEN(DATEDIFF(DAY, XV_Data, GETDATE())) <= 2 THEN 'Red'" + "  \n" +
                         "WHEN(DATEDIFF(DAY, XV_Data, GETDATE())) > 2  THEN 'DarkRed' END)" + "  \n" +
                         "WHEN JW_TransportMode = 'SEA' THEN(CASE WHEN(DATEDIFF(DAY, XV_Data, GETDATE())) <= 2 THEN 'Red' WHEN" + "  \n" +
                         "(DATEDIFF(DAY, XV_Data, GETDATE())) > 2   THEN 'DarkRed' END) ELSE NULL  END) AS COLORDIFF,GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName]" + "  \n" +
                         "FROM JobShipment" + "  \n" +
                         "left join GenCustomAddOnValue ON XV_ParentID = JS_PK and XV_Name = 'DO RELEASE TO CLIENT DT'" + "  \n" +
                         "inner join JobHeader on JH_ParentID = JS_PK" + "  \n" +
                         "left join JobConShipLink on  JN_JS = JS_PK" + "  \n" +
                         "left join JobConsol ON  JN_JK = JK_PK" + "  \n" +
                         "left join  JobConsolTransport ON   JK_PK = JW_ParentGUID" + "  \n" +
                         "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales" + "  \n" +
                         "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps" + "  \n" +
                         "where JS_IsCancelled = 0 and JS_IsValid = 1 and SUBSTRING(JS_UniqueConsignRef,9,1)= 'I' and XV_Name is null  " + "  \n" +
                        "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                    ")" + "  \n" +
                    "select ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep, Convert(varchar(11), CarrierDODate,106) as CarrierDODate,COLORDIFF as Status from a   " + "  \n" +
                    "select '' as AirGreen,'' as AirYellow,'0-2 Days' as AirRed,'2+ Days' as AirDarkRed,'' as SeaGreen,'' as SeaYellow,'0-2 Days' as SeaRed,'2+ Days' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.SIPending:
                    query =
                    ";with a as(  " + "  \n" +
                     "select distinct JS_UniqueConsignRef[ShipmentNumber],JH_GS_NKRepSales[SalesRep],JH_GS_NKRepOps[OperationRep],Jh_GC,JH_GB,GS1.GS_FullName[SalesRepName]," + "  \n" +
                     "GS2.GS_FullName[OperationRepName],Jw_Transportmode,jw_ETD," + "  \n" +
                     "(case when DATEDIFF(DAY, JW_ETD, GETDATE()) = -6 then 'Yellow'" + "  \n" +
                     "when DATEDIFF(DAY, JW_ETD, GETDATE())= -5 then 'Red' end ) as ColorStatus,DATEDIFF(DAY, JW_ETD, GETDATE()) daysDiff" + "  \n" +
                     "FROM JobShipment" + "  \n" +
                     "left join GenCustomAddOnValue ON XV_ParentID = JS_PK and XV_Name = 'SI DATE'" + "  \n" +
                     "inner join JobHeader on JH_ParentID = JS_PK" + "  \n" +
                     "left join JobConShipLink on  JN_JS = JS_PK" + "  \n" +
                     "left join JobConsol ON  JN_JK = JK_PK" + "  \n" +
                     "left join  JobConsolTransport ON   JK_PK = JW_ParentGUID" + "  \n" +
                     "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales" + "  \n" +
                     "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps" + "  \n" +
                     "where JS_IsCancelled = 0 and JS_IsValid = 1 and SUBSTRING(JS_UniqueConsignRef,8,2)= 'SE' and XV_Name is null " + "  \n" +
                        "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                    ")" + "  \n" +
                    "select ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep,Convert(varchar(11),jw_ETD,106)as ETDDate,ColorStatus as Status from a " + "  \n" +
                    "select '' as AirGreen,'' as AirYellow,'' as AirRed,'' as AirDarkRed,'' as SeaGreen,'-6 Days' as SeaYellow,'-5 Days' as SeaRed,'' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.InvoiceDispatchDetails:
                    query =
                    ";with a as(   " + "  \n" +
                    "select distinct JS_UniqueConsignRef[ShipmentNumber],JH_GS_NKRepSales[SalesRep],JH_GS_NKRepOps[OperationRep],Jh_GC,JH_GB,GS1.GS_FullName[SalesRepName], " + "  \n" +
                    "GS2.GS_FullName[OperationRepName], " + "  \n" +
                    "(case when JS_TransportMode = 'Air' then(case when datediff(day, ah_postDate, getdate()) < 1 then 'Green' when datediff(day, ah_postDate, getdate()) in(1, 2) then 'Yellow' " + "  \n" +
                                                                  "when datediff(day, ah_postDate, getdate())> 2 then 'Red' end) " + "  \n" +
                          "when Js_TransportMode = 'Sea' then(case when datediff(day, ah_postDate, getdate()) < 1 then 'Green' when datediff(day, ah_postDate, getdate()) in(1, 2) then 'Yellow' " + "  \n" +
                                                                  "when datediff(day, ah_postDate, getdate())> 2 then 'Red' end) end) as Status,Ah_PostDate " + "  \n" +
                     "FROM JobShipment " + "  \n" +
                     "left join GenCustomAddOnValue ON XV_ParentID = JS_PK and XV_Name = 'Invoice Dispatched Date' " + "  \n" +
                     "inner join JobHeader on JH_ParentID = JS_PK " + "  \n" +
                     "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales " + "  \n" +
                     "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps " + "  \n" +
                     "left join AccTransactionHeader at on at.AH_JH = JH_PK " + "  \n" +
                     "where JS_IsCancelled = 0 and JS_IsValid = 1  and XV_Name is null " + "  \n" +
                        "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                    ")" + "  \n" +
                    "select ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep,Convert(varchar(11),Ah_PostDate,106)as PostDate,Status from a " + "  \n" +
                    "select '0-1 Days' as AirGreen,'1-2 Days' as AirYellow,'2+ Days' as AirRed,'' as AirDarkRed,'0-1 Days' as SeaGreen,'1-2 Days' as SeaYellow,'2+ Days' as SeaRed,'' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.IGMNo:
                    query =
                        "; with a as ("+ "  \n" +
                        "select distinct JS_UniqueConsignRef[ShipmentNumber],JH_GS_NKRepSales[SalesRep],JH_GS_NKRepOps[OperationRep],Jh_GC,JH_GB,"+ "  \n" +
                        "GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName],jw_ETA,jw_TransportMode," + "  \n" +
                        "(case when jw_TransportMode= 'Air' then(CASE WHEN JW_ETA IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) = -6 THEN 'Yellow'" + "  \n" +
                        "WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) = -6  THEN 'Red' END) ELSE NULL END)" + "  \n" +
                        "when jw_TransportMode = 'Sea' then(CASE WHEN JW_ETA IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) = -6 THEN 'Yellow'" + "  \n" +
                        "WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) = -5  THEN 'Red' else null END) ELSE NULL END) END) as Status" + "  \n" +
                        "FROM JobShipment" + "  \n" +
                        "left join GenCustomAddOnValue ON XV_ParentID = JS_PK and XV_Name = 'IGM NO'" + "  \n" +
                        "inner join JobHeader on JH_ParentID = JS_PK" + "  \n" +
                        "left join JobConShipLink on  JN_JS = JS_PK" + "  \n" +
                        "left join JobConsol ON  JN_JK = JK_PK" + "  \n" +
                        "left join  JobConsolTransport ON   JK_PK = JW_ParentGUID" + "  \n" +
                        "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales" + "  \n" +
                        "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps" + "  \n" +
                        "where JS_IsCancelled = 0 and JS_IsValid = 1 and SUBSTRING(JS_UniqueConsignRef,8,2)in('AI', 'SI') and XV_Name is null" + "  \n" +
                        "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                    ")" + "  \n" +
                    "select ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep,Convert(varchar(11),jw_ETA,106) as ETADate,jw_TransportMode,Status from a  " + "  \n" +
                    "select '' as AirGreen,'-6 Days' as AirYellow,'-5 Days' as AirRed,'' as AirDarkRed,'' as SeaGreen,'-6 Days' as SeaYellow,'-5 Days' as SeaRed,'' as SeaDarkRed ";
                    break;
                case (int)DashboardFetchType.IGMFillingDT:
                    query =
                  "; with a as (" + "  \n" +
                        "select distinct JS_UniqueConsignRef[ShipmentNumber],JH_GS_NKRepSales[SalesRep],JH_GS_NKRepOps[OperationRep],Jh_GC,JH_GB," + "  \n" +
                        "GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName],jw_ETA,jw_TransportMode," + "  \n" +
                        "(case when jw_TransportMode= 'Air' then(CASE WHEN JW_ETA IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) = -6 THEN 'Yellow'" + "  \n" +
                        "WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) = -6  THEN 'Red' END) ELSE NULL END)" + "  \n" +
                        "when jw_TransportMode = 'Sea' then(CASE WHEN JW_ETA IS not NULL THEN(CASE WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) = -6 THEN 'Yellow'" + "  \n" +
                        "WHEN(DATEDIFF(DAY, JW_ETA, GETDATE())) = -5  THEN 'Red' else null END) ELSE NULL END) END) as Status" + "  \n" +
                        "FROM JobShipment" + "  \n" +
                        "left join GenCustomAddOnValue ON XV_ParentID = JS_PK and XV_Name = 'IGM NO'" + "  \n" +
                        "inner join JobHeader on JH_ParentID = JS_PK" + "  \n" +
                        "left join JobConShipLink on  JN_JS = JS_PK" + "  \n" +
                        "left join JobConsol ON  JN_JK = JK_PK" + "  \n" +
                        "left join  JobConsolTransport ON   JK_PK = JW_ParentGUID" + "  \n" +
                        "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales" + "  \n" +
                        "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps" + "  \n" +
                        "where JS_IsCancelled = 0 and JS_IsValid = 1 and SUBSTRING(JS_UniqueConsignRef,8,2)in('AI', 'SI') and XV_Name is null" + "  \n" +
                        "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                    ")" + "  \n" +
                    "select ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep,Convert(varchar(11),jw_ETA,106) as ETADate,jw_TransportMode,Status from a  " + "  \n" +
                    "select '' as AirGreen,'-6 Days' as AirYellow,'-5 Days' as AirRed,'' as AirDarkRed,'' as SeaGreen,'-6 Days' as SeaYellow,'-5 Days' as SeaRed,'' as SeaDarkRed ";

                    break;
                case (int)DashboardFetchType.VGMPending:
                    query =
              "  ;with a as(" + "  \n" +
                          "select distinct JS_UniqueConsignRef[ShipmentNumber], JH_GS_NKRepSales[SalesRep], JH_GS_NKRepOps[OperationRep], Jh_GC, JH_GB, jw_transportMode," + "  \n" +
                          "(case when DATEDIFF(DAY, JW_ETD, GETDATE()) = -6 then 'Yellow'" + "  \n" +
                          "when DATEDIFF(DAY, JW_ETD, GETDATE())= -5 then 'Red' end ) as Status,DATEDIFF(DAY, JW_ETD, GETDATE()) daysDiff" + "  \n" +
                          ",GS1.GS_FullName[SalesRepName],GS2.GS_FullName[OperationRepName],Jw_ETD" + "  \n" +
                          "from JobConShipLink" + "  \n" +
                          "inner join JobShipment ON JN_JS = JS_PK" + "  \n" +
                          "inner join JobConsol ON  JN_JK = JK_PK" + "  \n" +
                          "left join JobContainer ON  JK_PK = JC_PK" + "  \n" +
                          "left join JobHeader on JH_ParentID = JS_PK" + "  \n" +
                          "left join  JobConsolTransport ON   JK_PK = JW_ParentGUID" + "  \n" +
                          "left Join GlbStaff[GS1] on GS1.GS_Code = JobHeader.JH_GS_NKRepSales" + "  \n" +
                          "left Join GlbStaff[GS2] on GS2.GS_Code = JobHeader.JH_GS_NKRepOps" + "  \n" +
                          "where JS_IsCancelled = 0 and JS_IsValid = 1 and JK_IsCancelled = 0 and JK_IsValid = 1 and JC_GrossWeightVerificationDateTime is null" + "  \n" +
                           "and SUBSTRING(JS_UniqueConsignRef,8,2)= 'SI'                                                        " + "  \n" +
                          "and Jh_gc='" + CompanyCode + "' and Jh_GB='" + BranchCode + "'" + "  \n" +
                          ") " + "  \n" +
                          "select ShipmentNumber,(SalesRepName+' ('+SalesRep+')') as SalesRep,(OperationRepName+' ('+OperationRep+')') as OperationRep,Convert(nvarchar(11),Jw_ETD,106) as ETDDate,Status from a " + "  \n" +
                    "select '' as AirGreen,'' as AirYellow,'' as AirRed,'' as AirDarkRed,'' as SeaGreen,'-6 Days' as SeaYellow,'-5 Days' as SeaRed,'' as SeaDarkRed ";

                    break;
                default:
                    query = null;
                    break;
            }

            return GetResultInDataSet(query);
        }
    }
}
