using CashFlowControl.Core.Application.Security.Helpers;
using CashFlowControl.Core.Domain.Entities;
using Flunt.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowControl.Core.Application.Queries.Auth
{
    public class AuthGetRefreshTokenQuery : Notifiable<Notification>, IRequest<Result<RefreshToken?>>
    {
        public string RefreshToken;

        public AuthGetRefreshTokenQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
