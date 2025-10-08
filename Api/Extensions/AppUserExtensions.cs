using Api.DTOs;
using Api.Entities;
using Api.Interfaces;

namespace Api.Extensions
{
    public static class AppUserExtensions
    {
        public static UserResponse ToDTO(this AppUser user, ITokenService tokenService)
        {
            return new UserResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };
        }
    }
}