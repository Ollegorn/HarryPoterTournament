﻿using Entities.Entities;
using ServiceContracts.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IUserRepository
    {

        Task<User> GetUserByUsername(string username);

        Task<UserResponseDto> GetUserById(string id);

        Task<bool> UpdateUserPoints(Guid tournamentId, User user);

        Task<List<UserResponseDto>> GetAllUsers();

        Task<bool> UpdateUser(User user);

        //Task<bool> UpdateUserPointsByDuel(Duel duel);
    }
}
