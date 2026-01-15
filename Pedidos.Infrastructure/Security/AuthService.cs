using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pedidos.Application.DTOs;
using Pedidos.Application.Interfaces;
using Pedidos.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pedidos.Infrastructure.Security
{
    public class AuthService: IAuthService
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context, IConfiguration config)
        {
            _config = config;
            _context = context; 
        }

        public async Task<LoginResponseDto> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || user.Password != password) { 
                return new LoginResponseDto() { 
                    Success = false,
                    Message = "Usuario o clave no valida"
                };
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Rol)
                };

            var jwtSettings = _config.GetSection("Jwt");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"]));
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var expiresMinutesString = jwtSettings["ExpiresInMinutes"];
            var expiresMinutes = int.Parse(expiresMinutesString);

            return new LoginResponseDto
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = expiresMinutes * 60
            };
        }
    }
}
