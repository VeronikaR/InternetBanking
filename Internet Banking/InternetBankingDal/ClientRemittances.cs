//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
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
    }
}
