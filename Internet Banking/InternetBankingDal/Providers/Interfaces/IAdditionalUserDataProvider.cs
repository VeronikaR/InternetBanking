using System;
using System.Collections.Generic;

namespace InternetBankingDal.Providers.Interfaces
{
    public interface IAdditionalUserDataProvider 
    {
        bool AddUser(AdditionalUserData userData);
        bool DeleteUser(AdditionalUserData userData);

        AdditionalUserData GetUser(Guid userId);
        IEnumerable<AdditionalUserData> GetUsers();
    }
}
