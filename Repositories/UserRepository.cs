using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.UserDto;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _dbContext.Users.Where(u => u.UserName == username).SingleOrDefaultAsync();
            return user;
        }

        public async Task<bool> UpdateUserPoints(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.TotalTournamentPoints = user.TotalTournamentPoints;
            existingUser.Wins = user.Wins;
            existingUser.Defeats = user.Defeats;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            var usersResponseDto = users.Select(u => u.ToUserResponseDto()).ToList();

            return usersResponseDto;
        }

        //public async Task<bool> UpdateUserPointsByDuel(Duel duel)
        //{
        //    var existingUserOne = await _dbContext.Users.FindAsync(duel.UserOne.Id);
        //    var existingUserTwo = await _dbContext.Users.FindAsync(duel.UserTwo.Id);

        //    existingUserOne.CurrentTournamentPoints = duel.UserOne.CurrentTournamentPoints;
        //    existingUserTwo.CurrentTournamentPoints = duel.UserTwo.CurrentTournamentPoints;

        //    await _dbContext.SaveChangesAsync();

        //    return true;
        //}

    }
}
