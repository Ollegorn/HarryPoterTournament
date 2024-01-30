using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using RepositoryContracts;
using ServiceContracts.Interfaces.UserInterfaces;
using ServiceContracts.UserDto;

namespace Services.UserServices
{
    public class UserGetterService : IUserGetterService
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<User> _userManager;

        public UserGetterService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _repository = userRepository;
            _userManager = userManager;
        }

        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            var users = await _repository.GetAllUsers();
            return users;
        }

        public async Task<UserResponseDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            var userResponseDto = user.ToUserResponseDto();
            userResponseDto.Roles = roles;
            return userResponseDto;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _repository.GetUserByUsername(username);
            return user;
        }
    }
}
