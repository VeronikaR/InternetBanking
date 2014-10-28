using InternetBankingDal.Providers.Interfaces;

namespace InternetBankingDal.Providers.Implements
{
    public class AccountsProvider : GenericDataRepository<Accounts>, IAccountsProvider
    {
    }
}
