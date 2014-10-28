using InternetBankingDal;
using InternetBankingDal.Providers.Implements;
using Internet_Banking.Services.Interfaces;

namespace Internet_Banking.Services.Implements
{
    public class CardsService : GenericDataRepository<Cards>, ICardsService
    {
    }
}