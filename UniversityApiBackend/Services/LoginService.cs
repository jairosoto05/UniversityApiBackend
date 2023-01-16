using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class LoginService : ILoginService
    {
        private readonly UniversityDBContext _context;
        private readonly IConfiguration _config;

        public LoginService(UniversityDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<ServiceResponse<string>> LoginAsync(LoginUser loginUser)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var user = await _context.USERS!
                        .Where(u => u.Username == loginUser.Username).FirstOrDefaultAsync();

                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(loginUser.Password, user.PasswordHash))
                    {
                        var token = Generate(user);

                        _response.Success = true;
                        _response.Message = "ok";
                        _response.Data = token;
                        return _response;
                    }
                }
                _response.Success = false;
                _response.Message = "NotFound";
                

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null!;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;

        }
        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username!),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Rol.ToString()),
            };


            // Crear el token

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
