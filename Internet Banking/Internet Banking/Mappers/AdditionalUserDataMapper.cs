using Internet_Banking.Models;
using InternetBankingDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Internet_Banking.Mappers
{
    public static class AdditionalUserDataMapper
    {
        //Модель из Dal'a преобразовываем для модели в представлении
        public static AdditionalUserDataModel ToModel(AdditionalUserData userData)
        {
            MembershipUser user = Membership.GetUser(new Guid(userData.UserId.ToString()), false);
            var model = new AdditionalUserDataModel
            {
                UserName = user.UserName,
                LastName = userData.LastName,
                FirstName = userData.FirstName,
                MiddleName = userData.MiddleName,
                BirthDate = userData.BirthDate.ToShortDateString(),
                Nationality = userData.Nationality,
                IdentificationNumber = userData.IdentificationNumber,
                PassportNumber = userData.PassportNumber,
                Password = user.GetPassword()
            };
            return model;
        }

        public static AdditionalUserData FromModel(AdditionalUserDataModel userData)
        {
            DateTime birthDate = DateTime.Parse(userData.BirthDate);
            var user = new AdditionalUserData
            {
                Id = userData.UserId,
                UserId = userData.UserId,
                LastName = userData.LastName,
                FirstName = userData.FirstName,
                MiddleName = userData.MiddleName,
                BirthDate = birthDate,
                Nationality = userData.Nationality,
                IdentificationNumber = userData.IdentificationNumber,
                PassportNumber = userData.PassportNumber
            };
            return user;
        }
    }
}