using InternetBankingDal.Providers.Interfaces;

namespace InternetBankingDal.Providers.Implements
{
    public class CardsProvider : GenericDataRepository<Cards>, ICardsProvider
    {
    }
}
