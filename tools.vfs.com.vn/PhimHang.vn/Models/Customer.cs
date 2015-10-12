//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhimHang.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public Customer()
        {
            this.GroupDetails = new HashSet<GroupDetail>();
        }
    
        public string BranchCode { get; set; }
        public string ContractNumber { get; set; }
        public string CustomerId { get; set; }
        public int BrokerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameViet { get; set; }
        public string CustomerType { get; set; }
        public string DomesticForeign { get; set; }
        public Nullable<System.DateTime> Dob { get; set; }
        public string Sex { get; set; }
        public byte[] SignatureImage1 { get; set; }
        public byte[] SignatureImage2 { get; set; }
        public Nullable<System.DateTime> OpenDate { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public int CardType { get; set; }
        public string CardIdentity { get; set; }
        public Nullable<System.DateTime> CardDate { get; set; }
        public string CardIssuer { get; set; }
        public string Address { get; set; }
        public string AddressViet { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string Email { get; set; }
        public Nullable<int> UserCreate { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<int> UserModify { get; set; }
        public Nullable<System.DateTime> DateModify { get; set; }
        public Nullable<bool> ProxyStatus { get; set; }
        public string AccountStatus { get; set; }
        public string Notes { get; set; }
        public string WorkingAddress { get; set; }
        public Nullable<int> UserIntroduce { get; set; }
        public Nullable<int> AttitudePoint { get; set; }
        public Nullable<int> DepositPoint { get; set; }
        public Nullable<int> ActionPoint { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public string TaxCode { get; set; }
        public string AccountType { get; set; }
        public string OrderType { get; set; }
        public string ReceiveReport { get; set; }
        public string ReceiveReportBy { get; set; }
        public string MarriageStatus { get; set; }
        public string KnowledgeLevel { get; set; }
        public string Job { get; set; }
        public string OfficeFunction { get; set; }
        public string OfficeTel { get; set; }
        public string OfficeFax { get; set; }
        public string HusbandWifeName { get; set; }
        public string HusbandWifeCardNumber { get; set; }
        public Nullable<System.DateTime> HusbandWifeCardDate { get; set; }
        public string HusbandWifeCardLocation { get; set; }
        public string HusbandWifeTel { get; set; }
        public string HusbandWifeEmail { get; set; }
        public string JoinStockMarket { get; set; }
        public string InvestKnowledge { get; set; }
        public string InvestedIn { get; set; }
        public string InvestTarget { get; set; }
        public string RiskAccepted { get; set; }
        public string InvestFund { get; set; }
        public string DelegatePersonName { get; set; }
        public string DelegatePersonFunction { get; set; }
        public string DelegatePersonCardNumber { get; set; }
        public Nullable<System.DateTime> DelegateCardDate { get; set; }
        public string DelegateCardLocation { get; set; }
        public string DelegatePersonTel { get; set; }
        public string DelegatePersonEmail { get; set; }
        public string ChiefAccountancyName { get; set; }
        public string ChiefAccountancyCI { get; set; }
        public Nullable<System.DateTime> ChiefAccountancyCD { get; set; }
        public string ChiefAccountancyIssuer { get; set; }
        public byte[] ChiefAccountancySign1 { get; set; }
        public byte[] ChiefAccountancySign2 { get; set; }
        public string CaProxyName { get; set; }
        public string CaProxyCI { get; set; }
        public Nullable<System.DateTime> CaProxyCD { get; set; }
        public string CaProxyIssuer { get; set; }
        public byte[] CaProxySign1 { get; set; }
        public byte[] CaProxySign2 { get; set; }
        public byte[] CompanySign1 { get; set; }
        public byte[] CompanySign2 { get; set; }
        public string TradeCode { get; set; }
        public int CustomerAccount { get; set; }
        public string MobileSms { get; set; }
        public bool IsHere { get; set; }
        public string MoneyDepositeNumber { get; set; }
        public string MoneyDepositeLocation { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string PublicCompanyManage { get; set; }
        public string PublicCompanyHolder { get; set; }
        public string ParentCompanyName { get; set; }
        public string ParentCompanyAddress { get; set; }
        public string ParentCompanyEmail { get; set; }
        public string ParentCompanyTel { get; set; }
        public short PostType { get; set; }
        public Nullable<System.DateTime> ReOpenDate { get; set; }
        public Nullable<int> UserTakeCared { get; set; }
        public Nullable<int> TypeID { get; set; }
        public string SendYN { get; set; }
        public string ReceiveRelatedStockEmail { get; set; }
        public string ReceiveRelatedStockSms { get; set; }
        public Nullable<bool> VType { get; set; }
    
        public virtual CustomerType CustomerType1 { get; set; }
        public virtual ICollection<GroupDetail> GroupDetails { get; set; }
    }
}
