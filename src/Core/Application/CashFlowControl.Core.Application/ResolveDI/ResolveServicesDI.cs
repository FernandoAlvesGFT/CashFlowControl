﻿using CashFlowControl.Core.Application.Interfaces.Services;
using CashFlowControl.Core.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlowControl.Core.Application.ResolveDI
{
    public static class ResolveServicesDI
    {
        public static void RegistryServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddHttpClient<TransactionHttpClientService>();

            builder.Services.AddScoped<IDailyConsolidationService, DailyConsolidationService>();
            builder.Services.AddScoped<ITransactionHttpClientService, TransactionHttpClientService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
        }
    }
}
