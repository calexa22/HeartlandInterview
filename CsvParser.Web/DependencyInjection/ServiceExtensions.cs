using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using CsvParser.Web.Services;
using CsvParser.Web.Services.CsvProcessing;

namespace CsvParser.Web.DependencyInjection
{
    public static class ServiceExtensions
    {
        private const string API_KEY_SECTION = "ApiKeys";
        private const string API_BASE_ADDRESSES = "ApiBaseAddresses";

        private const string IGDB_KEY = "Igdb";
        private const string STEAM_KEY = "Steam";

        public static IServiceCollection ConfigureInternalDependencies(this IServiceCollection services/*, IConfiguration configuration*/)
        {
            services
                .AddTransient<IFileService, FileService>()
                .AddTransient<ISummaryService, SummaryService>()
                .AddTransient<IStudentCsvFileProcessor, StudentCsvProcessor>()
                .AddSingleton<IClock>(SystemClock.Instance);

            return services;
        }
    }
}
