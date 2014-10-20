using Internet_Banking.Filters;
using Internet_Banking.Mappers;
using Internet_Banking.Models;
using Internet_Banking.Services.Interfaces;
using InternetBankingDal;
using InternetBankingDal.Providers.Implements;
using InternetBankingDal.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Internet_Banking.Services
{
    public class UsersService : IUsersService
    {
        private const string USER_ROLE_NAME = "User";
        private IAdditionalUserDataProvider _additionalUserDataProvider;

        // Constructor
        public UsersService() {
            _additionalUserDataProvider = new AdditionalUserDataProvider();
        }

        // Generate password
        public string GeneratePassword()
        {
            return "111190Ihjkdff*%";  //TODO
        }

        // Get users
        public List<AdditionalUserDataModel> GetUsers()
        {
            List<AdditionalUserData> usersList = _additionalUserDataProvider.GetUsers().ToList();
            return usersList.Select(x => AdditionalUserDataMapper.ToModel(x)).ToList();
        }

        // Add user
        public bool AddUser(AdditionalUserDataModel model)
        {
            model.Password = GeneratePassword();

            Membership.CreateUser(model.UserName, model.Password);
            MembershipUser membershipUser = Membership.GetUser(model.UserName);
            if (membershipUser == null)
            {
                return false;
            }

            model.UserId = (Guid)membershipUser.ProviderUserKey;
            AdditionalUserData userData = AdditionalUserDataMapper.FromModel(model);
            userData.IsTemporary = true;
            _additionalUserDataProvider.AddUser(userData);
            Roles.AddUserToRole(model.UserName, USER_ROLE_NAME);
            return true;
        }

        // Delete user
        public bool DeleteUser(Guid userId)
        {
            AdditionalUserData additionalUserData = _additionalUserDataProvider.GetUser(userId);
            if (additionalUserData != null)
            {
                MembershipUser user = Membership.GetUser(additionalUserData.UserId, false);
                if (user != null)
                {
                    Membership.DeleteUser(user.UserName, true);
                }
                _additionalUserDataProvider.DeleteUser(additionalUserData);
            }
            return false;
        }
    }
}