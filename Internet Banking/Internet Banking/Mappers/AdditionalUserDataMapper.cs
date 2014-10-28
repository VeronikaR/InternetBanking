using Internet_Banking.Models;
using InternetBankingDal;
using System;
using System.Web.Security;

namespace Internet_Banking.Mappers
{
    public static class AdditionalUserDataMapper
    {
        //Модель из Dal'a преобразовываем для модели в представлении
        public static AdditionalUserDataModel ToModel(AdditionalUserData userData)
        {
            var user = Membership.GetUser(new Guid(userData.UserId.ToString()), false);
            if (user == null) return null;
            var model = new AdditionalUserDataModel
            {
                UserId = userData.UserId,
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
            var user = new AdditionalUserData
            {
                Id = userData.UserId,
                UserId = userData.UserId,
                LastName = userData.LastName,
                FirstName = userData.FirstName,
                MiddleName = userData.MiddleName,
                BirthDate = DateTime.Parse(userData.BirthDate),
                Nationality = userData.Nationality,
                IdentificationNumber = userData.IdentificationNumber,
                PassportNumber = userData.PassportNumber
            };
            return user;
        }
    }
}