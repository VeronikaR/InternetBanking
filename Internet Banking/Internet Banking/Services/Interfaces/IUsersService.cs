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

        AdditionalUserDataModel GetUser(Guid id);
        List<AdditionalUserDataModel> GetUsers();
        string AddUser(AdditionalUserDataModel model);
        bool DeleteUser(Guid userId);
    }
}
