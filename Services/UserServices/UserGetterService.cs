using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.Interfaces.UserInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public class UserGetterService : IUserGetterService
    {
        private readonly IUserRepository _repository;

        public UserGetterService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _repository.GetUserByUsername(username);
            
            return user;
        }
    }
}
