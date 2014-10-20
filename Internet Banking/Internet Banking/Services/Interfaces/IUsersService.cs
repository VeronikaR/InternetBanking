using Internet_Banking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Banking.Services.Interfaces
{
    interface IUsersService
    {
        string GeneratePassword();
        List<AdditionalUserDataModel> GetUsers();
        bool AddUser(AdditionalUserDataModel model);
        bool DeleteUser(Guid userId);
    }
}
