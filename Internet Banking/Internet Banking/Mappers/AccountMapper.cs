using System.Linq;
using Internet_Banking.Models;
using InternetBankingDal;
using System;
using System.Web.Security;
using Microsoft.Ajax.Utilities;

namespace Internet_Banking.Mappers
{
    public static class AccountMapper
    {
        public static SummaryAccountsModel ToModel(Accounts account)
        {
            var model = new SummaryAccountsModel
            {
                AccountId = account.AccountId,
                //Type = account.Type,
                Number = account.Number,
                ActiveCard = account.Cards.Count >= 1
            };
            return model;
        }

        public static Accounts FromModel(SummaryAccountsModel userData)
        {
            var user = new Accounts();
            //{
            //    Id = userData.UserId,
            //    UserId = userData.UserId,
            //    LastName = userData.LastName,
            //    FirstName = userData.FirstName,
            //    MiddleName = userData.MiddleName,
            //    BirthDate = DateTime.Parse(userData.BirthDate),
            //    Nationality = userData.Nationality,
            //    IdentificationNumber = userData.IdentificationNumber,
            //    PassportNumber = userData.PassportNumber
            //};
            return user;
        }

    }
}