using API.CreditCard.Database;
using System;

namespace API.CreditCard
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private ISqlDbConnection _connection = null;

        Guid _id = Guid.Empty;
        public UnitOfWork(ISqlDbConnection connection)
        {
            _id = Guid.NewGuid();
            _connection = connection;
        }

        ISqlDbConnection IUnitOfWork.Connection
        {
            get { return _connection; }
        }
     
        Guid IUnitOfWork.Id
        {
            get { return _id; }
        }

        public void Begin()
        {
             _connection.BeginTransactionAsync();
        }

        public void Commit()
        {
            _connection?.SqlTransaction?.Commit();
            _connection?.SqlConnection?.Close();
        }

        public void Rollback()
        {
            _connection?.SqlTransaction?.Rollback();
            _connection?.SqlConnection?.Close();
        }

        #region IDisposable implementation
        private bool disposed = false;
        public void Dispose()
        {
            if (!disposed)
            {
                if (_connection != null)
                    _connection.Dispose();

                disposed = true;
            }
        }
        #endregion
    }

    interface IUnitOfWork : IDisposable
    {
        Guid Id { get; }
        ISqlDbConnection Connection { get; }
        void Begin();
        void Commit();
        void Rollback();
    }
}
