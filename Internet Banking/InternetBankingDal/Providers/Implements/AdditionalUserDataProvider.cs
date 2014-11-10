using InternetBankingDal.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetBankingDal.Providers.Implements
{
    public class AdditionalUserDataProvider : IAdditionalUserDataProvider, IDisposable
    {
        private InternetBankingEntities _internetBankingEntities;

        public void Dispose()
        {
            if (_internetBankingEntities != null)
            {
                _internetBankingEntities.Dispose();
                _internetBankingEntities = null;
            }
        }
        public AdditionalUserDataProvider()
        {
            _internetBankingEntities = new InternetBankingEntities();
        }

        public bool AddUser(AdditionalUserData userData)
        {
            try
            {
                _internetBankingEntities.AdditionalUserData.Add(userData);
                _internetBankingEntities.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteUser(AdditionalUserData userData)
        {
            try
            {
                _internetBankingEntities.AdditionalUserData.Remove(userData);
                _internetBankingEntities.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public AdditionalUserData GetUser(Guid userId)
        {
            return _internetBankingEntities.AdditionalUserData.SingleOrDefault(x => x.UserId == userId);
        }

        public IEnumerable<AdditionalUserData> GetUsers()
        {
            return _internetBankingEntities.AdditionalUserData;
        }
    }
}