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
    
    public partial class ClientRemittances
    {
        public System.Guid ClientRemittanceId { get; set; }
        public System.Guid EndAccountId { get; set; }
        public System.Guid AccountOperationId { get; set; }
        public string RecipientName { get; set; }
    
        public virtual AccountOperations AccountOperations { get; set; }
        public virtual Accounts Accounts { get; set; }
    }
}