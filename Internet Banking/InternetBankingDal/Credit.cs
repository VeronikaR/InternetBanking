//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InternetBankingDal
{
    using System;
    using System.Collections.Generic;
    
    public partial class Credit
    {
        public Credit()
        {
            this.CreditOperations = new HashSet<CreditOperations>();
        }
    
        public System.Guid CreditId { get; set; }
        public System.Guid AccountId { get; set; }
        public string Number { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public System.DateTime NextPayDate { get; set; }
        public Nullable<decimal> SetCreditLimit { get; set; }
        public Nullable<decimal> MaxCreditLimit { get; set; }
        public decimal EndAmmount { get; set; }
        public System.Guid CreditPaymentId { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        public virtual CreditPayments CreditPayments { get; set; }
        public virtual ICollection<CreditOperations> CreditOperations { get; set; }
    }
}
