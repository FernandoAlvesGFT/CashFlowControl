using CashFlowControl.Core.Application.DTOs;
using CashFlowControl.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CashFlowControl.Core.Application.Interfaces.Services
{
    public interface IAuthService
    {
        string GenerateAccessToken(string userId);
        Task<RefreshToken> GenerateRefreshTokenAsync(string userId);
        Task<string> RefreshAccessTokenAsync(string refreshToken);
    }
}
