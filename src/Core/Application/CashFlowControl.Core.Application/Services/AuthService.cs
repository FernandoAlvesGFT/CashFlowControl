using CashFlowControl.Core.Application.Interfaces.Repositories;
using CashFlowControl.Core.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using CashFlowControl.Core.Application.Interfaces.Services;
using CashFlowControl.Core.Application.DTOs;
using Microsoft.AspNetCore.Identity;


namespace CashFlowControl.Core.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        public async Task<(IdentityResult? identityResult, ApplicationUser applicationUser)> RegisterUser(RegisterModelDTO model)
        {           
            return await _userRepository.RegisterUser(model);
        }

        public async Task<(SignInResult? result, ApplicationUser? user)> Authenticate(LoginModelDTO model)
        {
            return await _userRepository.Authenticate(model);
        }

        public string GenerateAccessToken(string userId)
        {
            var secretToken = _config["Jwt:SecretKey"] ?? string.Empty;
            var _issuer = _config["Jwt:Issuer"] ?? string.Empty;
            var _audience = _config["Jwt:Audience"] ?? string.Empty;


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretToken));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", userId)
        };

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(15), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(string userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)), 
                Expiration = DateTime.UtcNow.AddDays(7), 
                UserId = userId,
                IsRevoked = false
            };

            _userRepository.SaveRefreshToken(refreshToken); 
            return refreshToken;
        }

        public string RefreshAccessToken(string refreshToken)
        {
            var storedToken = _userRepository.GetRefreshToken(refreshToken);
            if (storedToken == null || storedToken.IsRevoked || storedToken.Expiration < DateTime.UtcNow)
                throw new Exception("Invalid refresh token.");

            return GenerateAccessToken(storedToken.UserId);
        }
    }
}
