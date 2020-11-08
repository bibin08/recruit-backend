using API.CreditCard.Config;
using Microsoft.Data.SqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.CreditCard.Database
{
    public interface ISqlDbConnection : IDisposable
    {
        Task<SqlConnection> OpenConnectionAsync(CancellationToken cancellationToken = default);
        SqlConnection SqlConnection { get; }
        SqlTransaction SqlTransaction { get; }
        Task<SqlTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    }
    public class SqlDbConnection : ISqlDbConnection
    {
        public IConnectionStringConfig Config { get; }
        public SqlConnection SqlConnection { get; private set; }

        public SqlTransaction SqlTransaction { get; private set; }

        public SqlDbConnection(IConnectionStringConfig config)
        {
            Config = config;
        }
        public async Task<SqlTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (SqlTransaction != null)
                return SqlTransaction;

            _ = await OpenConnectionAsync(cancellationToken);
            SqlTransaction = SqlConnection.BeginTransaction();
            return SqlTransaction;
        }

        #region IDisposable
        private bool disposed = false;
        public void Dispose()
        {
            if(!disposed)
            {
                if (SqlConnection != null)
                    SqlConnection.Dispose();

                if (SqlTransaction != null)
                    SqlTransaction.Dispose();

                disposed = true;
            }
        }

        #endregion

        public async Task<SqlConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
        {
            if (SqlConnection != null && SqlConnection.State == System.Data.ConnectionState.Open)
                return SqlConnection;

            SqlConnection = new SqlConnection(await Config.GetConnectionStringConfig(cancellationToken));
            await SqlConnection.OpenAsync(cancellationToken);
            return SqlConnection;

        }
    }
}
