using System;

namespace ICWR.Models
{
    public class UserDto
    { 
        public int SrNo { get; set; }
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string GuardianName { get; set; }
        public int? Age { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public int BranchId { get; set; }
        public string Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public string ZipCode { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string AccountNo { get; set; }
        public int? BankId { get; set; }
        public string Ifscode { get; set; }
        public string BankAddress { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string NomineeName { get; set; }
        public int? NomineeRelationId { get; set; }
        public int? NomineeAge { get; set; }
        public string NomineeAddress { get; set; }
        public string NomineeZipCode { get; set; }
        public long? NomineeCityId { get; set; }
        public string IntroducerCode { get; set; }
        public long IntroducerId { get; set; }
        public string UplineCode { get; set; }
        public long UplineID { get; set; }
        public string Password { get; set; }
        public bool IsVerify { get; set; }
        public DateTime? VerifyDate { get; set; }
        public long VerifyBy { get; set; }
        public string PaymentReceive { get; set; }
        public string ProductReceive { get; set; }
        public DateTime? ProductReceiveDate { get; set; }
        public string Position { get; set; }
        public int? UserTypeId { get; set; }
        public int? RankId { get; set; }
        public string Profile { get; set; }
        public string Aadhar { get; set; }
        public string AadharImage { get; set; }
        public string VoterId { get; set; }
        public string VoterIdImage { get; set; }
        public string DlNo { get; set; }
        public string DlNoImage { get; set; }
        public string Passport { get; set; }
        public string PassportImage { get; set; }
        public string PanCardNo { get; set; }
        public string PanCardImage { get; set; }
        public string BankImage { get; set; }
        public string PinNumber { get; set; }
        public int? PinId { get; set; }
        public int? PlanId { get; set; }
        public int? Level { get; set; }
        public bool IsActive { get; set; }
        public long? RptMgrID { get; set; }
        public bool IsCRM { get; set; }
        public string ApprovalType { get; set; }
        public long? LocationID { get; set; }


        // Reporting Parameter
        public string UserType { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CategoryName { get; set; }
        public float? Amount { get; set; }
        public int DirectCount { get; set; }
        public int TeamCount { get; set; }
        public int BV { get; set; }
        public bool IsScreenLock { get; set; }

        //EXTRA PROPERTIES
        public string Location { get; set; }
        public string ApproverMailID { get; set; }
    }

    
}