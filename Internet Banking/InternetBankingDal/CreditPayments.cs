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
    
    public partial class CreditPayments
    {
        public CreditPayments()
        {
            this.Credit = new HashSet<Credit>();
        }
    
        public System.Guid CreditPaymentId { get; set; }
        public decimal Debt { get; set; }
        public decimal OutDebt { get; set; }
        public decimal Percents { get; set; }
        public decimal Reward { get; set; }
        public decimal Fine { get; set; }
        public int Currency { get; set; }
    
        public virtual ICollection<Credit> Credit { get; set; }
    }
}
