using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using ServiceContracts.AuthorizationDto;
using System.Security.Claims;

namespace ServiceContracts.Interfaces
{
    public interface IJwtService
    {
        public Task<TokensResponseDto> GenerateJwtToken(User user);
        public Task<List<Claim>> GetAllValidClaims(User user);

        public Task<TokensResponseDto> VerifyAndGenerateToken(TokenRequestDto tokenRequest);
    }
}
