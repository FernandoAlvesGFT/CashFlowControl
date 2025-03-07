using Serilog;

namespace CashFlowControl.DailyConsolidation.Infrastructure.Logging
{
    public static class SerilogConfig
    {
        public static void Configuration()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
