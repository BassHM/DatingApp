using Api.Data;
using Api.DTOs;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AccountController(AppDbContext context) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register([FromBody] RegisterRequest dto)
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

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        private async Task<bool> EmailExists(string email)
        {
            return await context.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower()));
        }
    }
}