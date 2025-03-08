using CashFlowControl.Core.Application.DTOs;
using CashFlowControl.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CashFlowControl.Core.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<(IdentityResult? identityResult, ApplicationUser applicationUser)> RegisterUser(RegisterModelDTO model);
        Task<(SignInResult? result, ApplicationUser? user)> Authenticate(LoginModelDTO model);
        string GenerateAccessToken(string userId);
        RefreshToken GenerateRefreshToken(string userId);
        string RefreshAccessToken(string refreshToken);
    }
}
