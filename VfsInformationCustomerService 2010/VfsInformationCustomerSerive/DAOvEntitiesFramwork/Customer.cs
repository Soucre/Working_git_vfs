//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAOvEntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public int InvestorId { get; set; }
        public short BranchCode { get; set; }
        public short UnitCode { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public byte Type { get; set; }
        public byte PersonalType { get; set; }
        public System.DateTime Birthday { get; set; }
        public System.DateTime OpenDate { get; set; }
        public byte IdentityCardType { get; set; }
        public string IdentityCardNo { get; set; }
        public System.DateTime IdentityCardIssueDate { get; set; }
        public string IdentityCardIssueBy { get; set; }
        public string Address { get; set; }
        public string UserNameCreate { get; set; }
        public string NationalityName { get; set; }
        public string ContractNo { get; set; }
        public byte BalanceType { get; set; }
        public byte SecuritiesType { get; set; }
        public byte Status { get; set; }
        public byte Sex { get; set; }
        public string Flag { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool HasProxy { get; set; }
        public string IntroducedUser { get; set; }
        public string CaredUser { get; set; }
        public string IntroduceCustomerId { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public int MasterAccountId { get; set; }
        public int CustomerGroupId { get; set; }
        public string Notes { get; set; }
        public byte DepositType { get; set; }
        public int InfoChannelId { get; set; }
        public string ExternalIntroduceCustomerId { get; set; }
        public string Name2 { get; set; }
        public Nullable<bool> TradingStatus { get; set; }
        public string IntroducedUserType { get; set; }
    }
}
