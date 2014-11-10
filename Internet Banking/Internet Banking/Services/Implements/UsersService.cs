using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using InternetBankingDal;
using InternetBankingDal.Providers.Implements;
using InternetBankingDal.Providers.Interfaces;
using Internet_Banking.Mappers;
using Internet_Banking.Models;
using Internet_Banking.Services.Interfaces;

namespace Internet_Banking.Services.Implements
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
            return Membership.GeneratePassword(12, 2);
           // return "111190Ihjkdff*%";  
        }

        // Get users
        public List<AdditionalUserDataModel> GetUsers()
        {
            IList<AdditionalUserData> usersList = _additionalUserDataProvider.GetUsers().ToList();
            return usersList.Select(AdditionalUserDataMapper.ToModel).ToList();
        }

        public AdditionalUserDataModel GetUser(Guid id)
        {
            return AdditionalUserDataMapper.ToModel(_additionalUserDataProvider.GetUser(id));
        }
        // Add user
        public string AddUser(AdditionalUserDataModel model)
        {
            
            model.Password = GeneratePassword();

            Membership.CreateUser(model.UserName, model.Password);
            MembershipUser membershipUser = Membership.GetUser(model.UserName);
            //if (membershipUser == null)
            //{
            //    Membership.CreateUser(model.UserName, model.Password);
            //    return false;
            //}

            if (membershipUser != null && membershipUser.ProviderUserKey != null) 
                model.UserId = (Guid)membershipUser.ProviderUserKey;
            AdditionalUserData userData = AdditionalUserDataMapper.FromModel(model);
            userData.IsTemporary = true;
            _additionalUserDataProvider.AddUser(userData);
            Roles.AddUserToRole(model.UserName, USER_ROLE_NAME);
            return model.Password;
        }

        // Delete user
        public bool DeleteUser(Guid userId)
        {
            AdditionalUserData additionalUserData = _additionalUserDataProvider.GetUser(userId);
            if (additionalUserData != null)
            {
                //MembershipUser user = Membership.GetUser(additionalUserData.UserId, false);
                //if (user != null)
                //{
                //    Membership.DeleteUser(user.UserName, true);
                //}
                _additionalUserDataProvider.DeleteUser(additionalUserData);
            }
            return false;
        }
    }
}