using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetBankingDal;
using InternetBankingDal.Providers.Interfaces;
using Internet_Banking.Models;

namespace Internet_Banking.Services.Interfaces
{
    interface IAccountsService
    {
        List<SummaryAccountsModel> GetAccounts(Guid id);
    }
}
