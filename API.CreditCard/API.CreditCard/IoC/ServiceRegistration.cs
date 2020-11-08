using API.CreditCard.Config;
using API.CreditCard.CreditCard;
using API.CreditCard.Database;
using API.CreditCard.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CreditCard.IoC
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<RecruitBackendDbConnectionStringOptions>().Bind(configuration);
            services.AddOptions<EncryptionKeyOptions>().Bind(configuration);

            services.AddTransient<IConnectionStringConfig, ConnectionStringConfig>();

            services.AddScoped<ISqlDbConnection, SqlDbConnection>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenisationService, TokenisationHelper>();

            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
            services.AddTransient<ICreditCardService, CreditCardService>();

        }
    }
}
