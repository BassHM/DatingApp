using Api.Data;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AccountController(AppDbContext dbContext, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register([FromBody] RegisterRequest dto)
        {
            if (await EmailExists(dto.Email))
            {
                return BadRequest("Email is already taken");
            }

            using var hmac = new System.Security.Cryptography.HMACSHA512();

            var user = new AppUser
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return new UserResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginRequest dto)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email.ToLower().Equals(dto.Email.ToLower()));

            if (user == null) return Unauthorized("Invalid email or password");

            using var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt);

            var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dto.Password));

            for (int i = 0; i < computerHash.Length; i++)
            {
                if (computerHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid email or password");
            }

            return new UserResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };
        }

        private async Task<bool> EmailExists(string email)
        {
            return await dbContext.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower()));
        }
    }
}