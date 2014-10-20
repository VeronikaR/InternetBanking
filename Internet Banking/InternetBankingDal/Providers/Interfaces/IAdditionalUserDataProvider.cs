using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
