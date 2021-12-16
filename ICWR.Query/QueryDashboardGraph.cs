using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICWR.QueryInternal
{
    public class QueryDashboardGraph
    {

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

            string query = " --drop table #TRANSACTIONS1" + "  \n" +
"CREATE TABLE #TRANSACTIONS1                                                                                                           " + "  \n" +
"(                                                                                                                                     " + "  \n" +
"	TransactionPK UNIQUEIDENTIFIER,                                                                                                    " + "  \n" +
"	AccountPK UNIQUEIDENTIFIER,                                                                                                        " + "  \n" +
"	AccountCode nvarchar(12) COLLATE database_default,                                                                                 " + "  \n" +
"	AccountName nvarchar(100) COLLATE database_default,	                                                                               " + "  \n" +
"	BranchCode CHAR(3) COLLATE database_default,                                                                                       " + "  \n" +
"	CountryCode CHAR(2) COLLATE database_default,                                                                                      " + "  \n" +
"	CountryName nvarchar(80) COLLATE database_default,                                                                                 " + "  \n" +
"	TransactionType Char(3) COLLATE database_default,                                                                                  " + "  \n" +
"	InvoiceRef varchar(38) COLLATE database_default,                                                                                   " + "  \n" +
"	Description nvarchar(128) COLLATE database_default,                                                                                " + "  \n" +
"	InvoiceRef2 nvarchar(80) COLLATE database_default,                                                                                 " + "  \n" +
"	ContactInfo varchar(200) COLLATE database_default,                                                                                 " + "  \n" +
"	ContactName nvarchar(256) COLLATE database_default,                                                                                " + "  \n" +
"	ContactPhoneNo varchar(20) COLLATE database_default,                                                                               " + "  \n" +
"	OC_OH_AddressOverride UNIQUEIDENTIFIER,                                                                                            " + "  \n" +
"	InvoiceTotal money DEFAULT 0,                                                                                                      " + "  \n" +
"	Balance money DEFAULT 0,                                                                                                           " + "  \n" +
"	BalanceInLocal money DEFAULT 0,                                                                                                    " + "  \n" +
"	DueDate smalldatetime,                                                                                                             " + "  \n" +
"	InvoiceDate smalldatetime,                                                                                                         " + "  \n" +
"	CurrencyPK UNIQUEIDENTIFIER,                                                                                                       " + "  \n" +
"	CurrencyCode char(3) COLLATE database_default,                                                                                     " + "  \n" +
"	CurrencyCode2 char(3) COLLATE database_default,                                                                                    " + "  \n" +
"	NotDue1Total money,                                                                                                                " + "  \n" +
"	NotDue2Total money,                                                                                                                " + "  \n" +
"	NotDue3Total money,                                                                                                                " + "  \n" +
"	NotDue4Total money,                                                                                                                " + "  \n" +
"	Period1Total money,                                                                                                                " + "  \n" +
"	Period2Total money,                                                                                                                " + "  \n" +
"	Period3Total money,                                                                                                                " + "  \n" +
"	Period4Total money,                                                                                                                " + "  \n" +
"	PeriodCurrent money,                                                                                                               " + "  \n" +
"	NotDue1TotalInLocal money,                                                                                                         " + "  \n" +
"	NotDue2TotalInLocal money,                                                                                                         " + "  \n" +
"	NotDue3TotalInLocal money,                                                                                                         " + "  \n" +
"	NotDue4TotalInLocal money,                                                                                                         " + "  \n" +
"	Period1TotalInLocal money,                                                                                                         " + "  \n" +
"	Period2TotalInLocal money,                                                                                                         " + "  \n" +
"	Period3TotalInLocal money,                                                                                                         " + "  \n" +
"	Period4TotalInLocal money,                                                                                                         " + "  \n" +
"	PeriodCurrentInLocal money,                                                                                                        " + "  \n" +
"	SalesRep char(3) COLLATE database_default,                                                                                         " + "  \n" +
"	CreditController char(3) COLLATE database_default,                                                                                 " + "  \n" +
"	CustomerService char(3) COLLATE database_default,                                                                                  " + "  \n" +
"	CreditLimit money DEFAULT 0,                                                                                                       " + "  \n" +
"	AccountGroup nvarchar(6) COLLATE database_default,                                                                                 " + "  \n" +
"	OrgBranchCode char(3) COLLATE database_default,                                                                                    " + "  \n" +
"	IsOverLimit char(1) COLLATE database_default,                                                                                      " + "  \n" +
"	ExchangeRate decimal(18, 9),                                                                                                       " + "  \n" +
"	IsDSBInvoice char(3) COLLATE database_default,                                                                                     " + "  \n" +
"	DSBCharge money,                                                                                                                   " + "  \n" +
"	OSTotal money,                                                                                                                     " + "  \n" +
"	ARCategory char(3) COLLATE database_default,                                                                                       " + "  \n" +
"	ConsolidationCategory char(3) COLLATE database_default,                                                                            " + "  \n" +
"	SettlementCode varchar(12) COLLATE database_default,                                                                               " + "  \n" +
"	AccountCode2 nvarchar(12) COLLATE database_default,                                                                                " + "  \n" +
"	OrgBranchName varchar(100) COLLATE database_default,                                                                               " + "  \n" +
"	TranBranchName varchar(100) COLLATE database_default,                                                                              " + "  \n" +
"	SalesRepName nvarchar(256) COLLATE database_default,                                                                               " + "  \n" +
"	CreditControllerName nvarchar(256) COLLATE database_default,                                                                       " + "  \n" +
"	CustomerServiceName nvarchar(256) COLLATE database_default,                                                                        " + "  \n" +
"	SettlementName varchar(100) COLLATE database_default,	                                                                           " + "  \n" +
"	RXSubUnitRatio int,                                                                                                                " + "  \n" +
"	PostDate smalldatetime,                                                                                                            " + "  \n" +
"	DepartmentCode char(3) COLLATE database_default,                                                                                   " + "  \n" +
"	FullyPaidDate smalldatetime,                                                                                                       " + "  \n" +
"	InvoiceTerm char(15) COLLATE database_default,                                                                                     " + "  \n" +
"	InvoiceExTax money,                                                                                                                " + "  \n" +
"	TaxAmount money,                                                                                                                   " + "  \n" +
"	JobNumber varchar(35)COLLATE database_default,                                                                                     " + "  \n" +
"	TransactionTypeDesc varchar(20)COLLATE database_default,                                                                           " + "  \n" +
"	OperatorInitials VARCHAR(3) COLLATE database_default,                                                                              " + "  \n" +
"	LineInvoiceExTaxAmount money,                                                                                                      " + "  \n" +
"	LineTaxAmount money,                                                                                                               " + "  \n" +
"	ARUseSettlementGroupCreditLimit char(1),                                                                                           " + "  \n" +
"	SettlementGroupCreditLimit money,                                                                                                  " + "  \n" +
"	SettlementGroupOutstandingAmount money,                                                                                            " + "  \n" +
"	SettlementGroupOverCreditLimit char(1),                                                                                            " + "  \n" +
"	UsingOrgAsOwnSettlementGroup char(1),                                                                                              " + "  \n" +
"	AgreedPaymentMethod varchar(3),                                                                                                    " + "  \n" +
"	RX_PK	uniqueidentifier,                                                                                                          " + "  \n" +
"RX_Code	nvarchar(100),                                                                                                             " + "  \n" +
"RX_Symbol	nvarchar(100),                                                                                                             " + "  \n" +
"RX_Desc	nvarchar(100),                                                                                                             " + "  \n" +
"RX_UnitName	nvarchar(100),                                                                                                         " + "  \n" +
"RX_SubUnitName	nvarchar(100),                                                                                                         " + "  \n" +
"RX_SubUnitRatio	int,                                                                                                               " + "  \n" +
"RX_IsActive	nvarchar(100),                                                                                                         " + "  \n" +
"RX_IsSystem	nvarchar(100),                                                                                                         " + "  \n" +
"RE_PK	uniqueidentifier,                                                                                                              " + "  \n" +
"RE_ExRateType	nvarchar(100),                                                                                                         " + "  \n" +
"RE_StartDate	datetime,                                                                                                              " + "  \n" +
"RE_ExpiryDate	datetime,                                                                                                              " + "  \n" +
"RE_SellRate	decimal(18,2),                                                                                                         " + "  \n" +
"RE_RX_NKExCurrency	nvarchar(100),                                                                                                     " + "  \n" +
"RE_OH_Client	nvarchar(100),                                                                                                         " + "  \n" +
"RE_GC	nvarchar(100),                                                                                                                 " + "  \n" +
"RE_IsSystem	nvarchar(100),                                                                                                         " + "  \n" +
"PeriodEndRate	decimal(18,2),                                                                                                         " + "  \n" +
"RevaluedLocalEquivalent	nvarchar(100),                                                                                             " + "  \n" +
"GainLossOnRevaluation	nvarchar(100),                                                                                                 " + "  \n" +
"Company_Pk uniqueidentifier,                                                                                                          " + "  \n" +
"CompanyName varchar(200),                                                                                                             " + "  \n" +
"LedgerAccount varchar(4)                                                                                                              " + "  \n" +
")                                                                                                                                     " + "  \n" +
"                                                                                                                                      " + "  \n" +
"declare @Company uniqueidentifier                                                                                                     " + "  \n" +
"declare @CompanyName varchar(5000)                                                                                                    " + "  \n" +
"                                                                                                                                      " + "  \n" +
"                                                                                                                                      " + "  \n" +
"declare cur cursor for                                                                                                                " + "  \n" +
"select GC_PK,GC_Name From dbo.GlbCompany                                                                                              " + "  \n" +
"                                                                                                                                      " + "  \n" +
"open cur                                                                                                                              " + "  \n" +
"fetch next from cur into @Company,@CompanyName                                                                                        " + "  \n" +
"while @@fetch_status=0                                                                                                                " + "  \n" +
"begin                                                                                                                                 " + "  \n" +
"   declare @Period varchar(7)='201812',@datetie datetime= getdate()                                                                   " + "  \n" +
"                                                                                                                                      " + "  \n" +
"   insert into #TRANSACTIONS1                                                                                                         " + "  \n" +
"   (TransactionPK ,                                                                                                                   " + "  \n" +
"	AccountPK ,                                                                                                                        " + "  \n" +
"	AccountCode ,                                                                                                                      " + "  \n" +
"	AccountName ,	                                                                                                                   " + "  \n" +
"	BranchCode            ,                                                                                                            " + "  \n" +
"	CountryCode			  ,                                                                                                            " + "  \n" +
"	CountryName			  ,                                                                                                            " + "  \n" +
"	TransactionType		  ,                                                                                                            " + "  \n" +
"	InvoiceRef			  ,                                                                                                            " + "  \n" +
"	Description 		  ,                                                                                                            " + "  \n" +
"	InvoiceRef2 		  ,                                                                                                            " + "  \n" +
"	ContactInfo 		  ,                                                                                                            " + "  \n" +
"	ContactName 		  ,                                                                                                            " + "  \n" +
"	ContactPhoneNo 		  ,                                                                                                            " + "  \n" +
"	OC_OH_AddressOverride ,                                                                                                            " + "  \n" +
"	InvoiceTotal 		  ,                                                                                                            " + "  \n" +
"	Balance 			  ,                                                                                                            " + "  \n" +
"	BalanceInLocal		  ,                                                                                                            " + "  \n" +
"	DueDate 			  ,                                                                                                            " + "  \n" +
"	InvoiceDate 		  ,                                                                                                            " + "  \n" +
"	CurrencyPK			  ,                                                                                                            " + "  \n" +
"	CurrencyCode 		  ,                                                                                                            " + "  \n" +
"	CurrencyCode2		  ,                                                                                                            " + "  \n" +
"	NotDue1Total ,                                                                                                                     " + "  \n" +
"	NotDue2Total ,                                                                                                                     " + "  \n" +
"	NotDue3Total ,                                                                                                                     " + "  \n" +
"	NotDue4Total ,                                                                                                                     " + "  \n" +
"	Period1Total ,                                                                                                                     " + "  \n" +
"	Period2Total ,                                                                                                                     " + "  \n" +
"	Period3Total ,                                                                                                                     " + "  \n" +
"	Period4Total ,                                                                                                                     " + "  \n" +
"	PeriodCurrent ,                                                                                                                    " + "  \n" +
"	NotDue1TotalInLocal ,                                                                                                              " + "  \n" +
"	NotDue2TotalInLocal ,                                                                                                              " + "  \n" +
"	NotDue3TotalInLocal ,                                                                                                              " + "  \n" +
"	NotDue4TotalInLocal ,                                                                                                              " + "  \n" +
"	Period1TotalInLocal ,                                                                                                              " + "  \n" +
"	Period2TotalInLocal ,                                                                                                              " + "  \n" +
"	Period3TotalInLocal ,                                                                                                              " + "  \n" +
"	Period4TotalInLocal ,                                                                                                              " + "  \n" +
"	PeriodCurrentInLocal ,                                                                                                             " + "  \n" +
"	SalesRep ,                                                                                                                         " + "  \n" +
"	CreditController ,                                                                                                                 " + "  \n" +
"	CustomerService ,                                                                                                                  " + "  \n" +
"	CreditLimit ,                                                                                                                      " + "  \n" +
"	AccountGroup ,                                                                                                                     " + "  \n" +
"	OrgBranchCode ,                                                                                                                    " + "  \n" +
"	IsOverLimit ,                                                                                                                      " + "  \n" +
"	ExchangeRate ,                                                                                                                     " + "  \n" +
"	IsDSBInvoice ,                                                                                                                     " + "  \n" +
"	DSBCharge,                                                                                                                         " + "  \n" +
"	OSTotal,                                                                                                                           " + "  \n" +
"	ARCategory ,                                                                                                                       " + "  \n" +
"	ConsolidationCategory ,                                                                                                            " + "  \n" +
"	SettlementCode  ,                                                                                                                  " + "  \n" +
"	AccountCode2 	,                                                                                                                  " + "  \n" +
"	OrgBranchName 	,                                                                                                                  " + "  \n" +
"	TranBranchName	,                                                                                                                  " + "  \n" +
"	SalesRepName 	,                                                                                                                  " + "  \n" +
"	CreditControllerName,                                                                                                              " + "  \n" +
"	CustomerServiceName,                                                                                                               " + "  \n" +
"	SettlementName ,                                                                                                                   " + "  \n" +
"	RXSubUnitRatio,                                                                                                                    " + "  \n" +
"	PostDate ,                                                                                                                         " + "  \n" +
"	DepartmentCode ,                                                                                                                   " + "  \n" +
"	FullyPaidDate ,                                                                                                                    " + "  \n" +
"	InvoiceTerm ,                                                                                                                      " + "  \n" +
"	InvoiceExTax ,                                                                                                                     " + "  \n" +
"	TaxAmount ,                                                                                                                        " + "  \n" +
"	JobNumber ,                                                                                                                        " + "  \n" +
"	TransactionTypeDesc ,                                                                                                              " + "  \n" +
"	OperatorInitials ,                                                                                                                 " + "  \n" +
"	LineInvoiceExTaxAmount ,                                                                                                           " + "  \n" +
"	LineTaxAmount ,                                                                                                                    " + "  \n" +
"	ARUseSettlementGroupCreditLimit ,                                                                                                  " + "  \n" +
"	SettlementGroupCreditLimit ,                                                                                                       " + "  \n" +
"	SettlementGroupOutstandingAmount ,                                                                                                 " + "  \n" +
"	SettlementGroupOverCreditLimit ,                                                                                                   " + "  \n" +
"	UsingOrgAsOwnSettlementGroup ,                                                                                                     " + "  \n" +
"	AgreedPaymentMethod ,                                                                                                              " + "  \n" +
"	RX_PK	,                                                                                                                          " + "  \n" +
"RX_Code	,                                                                                                                          " + "  \n" +
"RX_Symbol,	                                                                                                                           " + "  \n" +
"RX_Desc	,                                                                                                                          " + "  \n" +
"RX_UnitName	,                                                                                                                      " + "  \n" +
"RX_SubUnitName	,                                                                                                                      " + "  \n" +
"RX_SubUnitRatio	,                                                                                                                  " + "  \n" +
"RX_IsActive	,                                                                                                                      " + "  \n" +
"RX_IsSystem	,                                                                                                                      " + "  \n" +
"RE_PK	,                                                                                                                              " + "  \n" +
"RE_ExRateType	,                                                                                                                      " + "  \n" +
"RE_StartDate	,                                                                                                                      " + "  \n" +
"RE_ExpiryDate	,                                                                                                                      " + "  \n" +
"RE_SellRate	,                                                                                                                      " + "  \n" +
"RE_RX_NKExCurrency	,                                                                                                                  " + "  \n" +
"RE_OH_Client	,                                                                                                                      " + "  \n" +
"RE_GC	,                                                                                                                              " + "  \n" +
"RE_IsSystem	,                                                                                                                      " + "  \n" +
"PeriodEndRate	,                                                                                                                      " + "  \n" +
"RevaluedLocalEquivalent	,                                                                                                          " + "  \n" +
"GainLossOnRevaluation	)                                                                                                              " + "  \n" +
"                                                                                                                                      " + "  \n" +
"	exec ARAPTransactionsSP @Period ,@Company  ,@OrgList ='' ,@OrgGroupList ='' ,@BranchList ='' ,@CountryList ='' ,@ExCountryList ='' ,@SalesRepList ='' " + "  \n" +
", @OverLimitOnly ='' ,@AgeingOption ='PER' , @Day1 =0 , @Day2 =0 , @Day3 =0 , @Day4 =0 , @AccountsRelationShip ='' , @ConsolidatedCategory =''                   " + "  \n" +
", @SalesRepRoll ='' , @SummaryOnly ='' , @LedgerType ='AP'  ,@AgedByInvoiceDate ='NON' , @CurrencyList ='' , @IncludeDisbursement ='' ,@DisbursementTranOnly =''  " + "  \n" +
", @SettlementGroupList ='' , @CreditRating ='' , @ShowInInvoicedCurrency =''  ,@ShowLocalEquivalentTotal ='' , @ShowAllTransactions ='Y' , @PaymentStatus =''  " + "  \n" +
", @TransactionTypeList ='INV'  , @PostDateFrom = null , @PostDateTo = null , @DueDateFrom = null ,@DueDateTo = null , @InvoiceDateFrom = null  " + "  \n" +
",@InvoiceDateTo = null , @NotInActiveBatchTranOnly ='' , @GroupBy ='Transaction Type' , @OrderBy ='' , @OrgBranch ='' , @ShowAddUser ='' ,@ShowOnlyAggregated =''  " + "  \n" +
", @ShowLineAmounts ='' , @CurrentDateTime = @datetie, @OverdueTransactions = '1' , @BranchManagementCode = NULL , @AgreedPaymentMethodList = '' " + "  \n" +
"                                                                                                                                           " + "  \n" +
"update #TRANSACTIONS1 set Company_Pk=@Company,CompanyName = @CompanyName,LedgerAccount='AP' where Company_Pk is null                       " + "  \n" +
"                                                                                                                                           " + "  \n" +
"   fetch next from cur into @Company,@CompanyName                                                                                          " + "  \n" +
"end                                                                                                                                        " + "  \n" +
"close cur                                                                                                                                  " + "  \n" +
"deallocate cur                                                                                                                             " + "  \n" +
"                                                                                                                                           " + "  \n" +
"--select * from #TRANSACTIONS1                                                                                                             " + "  \n" +
"                                                                                                                                           " + "  \n" +
"select                                                                                                                                     " + "  \n" +
"	AccountName ,	                                                                                                                        " + "  \n" +
"	BranchCode            ,                                                                                                                 " + "  \n" +
"	CountryCode			  ,                                                                                                                 " + "  \n" +
"	CountryName			  ,                                                                                                                 " + "  \n" +
"	TransactionType		  ,                                                                                                                 " + "  \n" +
"	DepartmentCode,                                                                                                                         " + "  \n" +
"	InvoiceRef2,                                                                                                                            " + "  \n" +
"	InvoiceTotal 		  ,                                                                                                                 " + "  \n" +
"	Balance 			  ,                                                                                                                 " + "  \n" +
"	BalanceInLocal		  ,                                                                                                                 " + "  \n" +
"	DueDate 			  ,                                                                                                                 " + "  \n" +
"	InvoiceDate 		  ,                                                                                                                 " + "  \n" +
"	SalesRep ,                                                                                                                              " + "  \n" +
"	CreditLimit ,                                                                                                                           " + "  \n" +
"	AccountGroup ,                                                                                                                          " + "  \n" +
"	OrgBranchCode ,                                                                                                                         " + "  \n" +
"	ExchangeRate ,                                                                                                                          " + "  \n" +
"	OSTotal,                                                                                                                                " + "  \n" +
"	SettlementCode  ,                                                                                                                       " + "  \n" +
"	AccountCode2 	,                                                                                                                       " + "  \n" +
"	OrgBranchName 	,                                                                                                                       " + "  \n" +
"	TranBranchName	,                                                                                                                       " + "  \n" +
"	SalesRepName 	,                                                                                                                       " + "  \n" +
"	SettlementName ,                                                                                                                        " + "  \n" +
"	PostDate ,                                                                                                                              " + "  \n" +
"	SettlementGroupCreditLimit ,                                                                                                            " + "  \n" +
"	SettlementGroupOutstandingAmount ,                                                                                                      " + "  \n" +
"	CompanyName,                                                                                                                            " + "  \n" +
"	LedgerAccount                                                                                                                           " + "  \n" +
" from #TRANSACTIONS1                                                                                                                       ";




            return QueryInternal.GetResult(query);
            //SELECT SUM(OutStandingTotal) FROM @OUTPUT  
        }

    }
}
