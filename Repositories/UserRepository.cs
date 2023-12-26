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

        public async Task<bool> UpdateUserPoints(Guid tournamentId, User user)
        {
            var existingUser = await _dbContext.Users
                .Include(u => u.TournamentStats)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser == null)
            {
                return false;
            }

            var tournamentStats = existingUser.TournamentStats
                .FirstOrDefault(ts => ts.TournamentId == tournamentId);

            if (tournamentStats == null)
            {
                return false;
            }

            tournamentStats.TotalPoints = user.TournamentStats
                .FirstOrDefault(ts => ts.TournamentId == tournamentId)?.TotalPoints ?? 0;

            tournamentStats.Wins = user.TournamentStats
                .FirstOrDefault(ts => ts.TournamentId == tournamentId)?.Wins ?? 0;

            tournamentStats.Defeats = user.TournamentStats
                .FirstOrDefault(ts => ts.TournamentId == tournamentId)?.Defeats ?? 0;

            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            var users = await _dbContext.Users
                .Include(u => u.TournamentStats)
                .Include(u => u.ReceivedInvitations)
                .Include(u => u.SentInvitations)
                .ToListAsync();

            var usersResponseDto = users.Select(u => u.ToUserResponseDto()).ToList();
            return usersResponseDto;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            
            if (userToUpdate == null) 
               return false;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserResponseDto> GetUserById(string id)
        {
            var user = await _dbContext.Users
                .Include(u => u.TournamentStats)
                .Include(u => u.ReceivedInvitations)
                .Include(u => u.SentInvitations)
                .FirstOrDefaultAsync(u => u.Id == id);

            var userResponse = user.ToUserResponseDto();
            return userResponse;
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
