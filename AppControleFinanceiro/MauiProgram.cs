using AppControleFinanceiro.Repositories;
using AppControleFinanceiro.Views;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace AppControleFinanceiro
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterDatabaseAndRepositories()
                .regiserviews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static MauiAppBuilder RegisterDatabaseAndRepositories(this MauiAppBuilder MauiAppBuilder)
        {
            MauiAppBuilder.Services.AddSingleton<LiteDatabase>(options =>
            {
                return new LiteDatabase(AppSettings.FullDatabasePath);
            });
            MauiAppBuilder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
            return MauiAppBuilder;
        }
        public static MauiAppBuilder regiserviews(this MauiAppBuilder MauiAppBuilder)
        {
            MauiAppBuilder.Services.AddTransient<TransactionAdd>();
            MauiAppBuilder.Services.AddTransient<TransactionEdit>();
            MauiAppBuilder.Services.AddTransient<TransactionList>();
            return MauiAppBuilder;
        }
    }
}
