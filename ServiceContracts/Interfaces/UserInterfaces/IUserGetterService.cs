using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.UserInterfaces
{
    public interface IUserGetterService
    {
        Task<User> GetUserByUsername(string username);
    }
}
