using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.CreditCard.Config
{
    public interface IConnectionStringConfig
    {
        Task<string> GetConnectionStringConfig(CancellationToken cancellationToken = default);
    }

    public class ConnectionStringConfig : IConnectionStringConfig
    {
        private RecruitBackendDbConnectionStringOptions _connectionStringOptions;

        public ConnectionStringConfig(IOptionsMonitor<RecruitBackendDbConnectionStringOptions> connectionStringOptions)
        {
            _connectionStringOptions = connectionStringOptions.CurrentValue;
        }
        public async Task<string> GetConnectionStringConfig(CancellationToken cancellationToken = default)
        {
            return _connectionStringOptions.RecruitBackendDb;
        }
    }
}
