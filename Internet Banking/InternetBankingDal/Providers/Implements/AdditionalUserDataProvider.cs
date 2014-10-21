using InternetBankingDal.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingDal.Providers.Implements
{
    public class AdditionalUserDataProvider : IAdditionalUserDataProvider
    {
        private InternetBankingEntities _internetBankingEntities;

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
            catch (Exception e) 
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
            return _internetBankingEntities.AdditionalUserData.SingleOrDefault(x => x.Id == userId);
        }

        public IEnumerable<AdditionalUserData> GetUsers()
        {
            return _internetBankingEntities.AdditionalUserData;
        }
    }
}