using EmpAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAPI.Repositories
{
    public interface IAuthentication
    {
        UserModel AuthenticateUser(UserModel model);

        string GenerateJSONWebToken(UserModel model);

        string RegisterUser(UserModel model);
    }
}
