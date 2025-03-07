using System.Net;

namespace CashFlowControl.LaunchControl.API.Configurations
{
    public static class ConfigureKestrel
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var certPath = builder.Configuration["Kestrel:Certificates:Default:Path"] ?? "";
            var certPassword = builder.Configuration["Kestrel:Certificates:Default:Password"] ?? "";

            if (string.IsNullOrEmpty(certPath) || string.IsNullOrEmpty(certPassword))
            {
                throw new InvalidOperationException("Certificado ou senha não configurados.");
            }

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(7253, listenOptions =>
                {
                    listenOptions.UseHttps(certPath, certPassword);
                });
            });
        }
    }
}
