using InternetBankingDal;
using InternetBankingDal.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Banking.Services.Interfaces
{
    interface ICardsService : IGenericDataRepository<Cards>
    {
    }
}
