using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using InternetBankingDal;
using InternetBankingDal.Providers.Implements;
using InternetBankingDal.Providers.Interfaces;
using Internet_Banking.Mappers;
using Internet_Banking.Models;
using Internet_Banking.Services.Interfaces;

namespace Internet_Banking.Services.Implements
{
    public class AccountsService : IAccountsService
    {
        private IAccountsProvider _accountsProvider;

        public AccountsService()
        {
            _accountsProvider = new AccountsProvider();
        }

        public List<SummaryAccountsModel> GetAccounts(Guid id)
        {
            IList<Accounts> accounts = _accountsProvider.GetList(x => x.UserId == id);//.GetUsers().ToList();
            return accounts.Select(x => AccountMapper.ToModel(x)).ToList();
        }
    }
}