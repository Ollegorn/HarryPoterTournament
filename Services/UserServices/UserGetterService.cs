using Entities.Entities;
using RepositoryContracts;
using ServiceContracts.Interfaces.UserInterfaces;
using ServiceContracts.UserDto;
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

        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            var users = await _repository.GetAllUsers();
            return users;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _repository.GetUserByUsername(username);
            
            return user;
        }
    }
}
