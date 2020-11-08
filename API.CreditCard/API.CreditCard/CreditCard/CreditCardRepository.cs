using API.CreditCard.CreditCard.Models;
using API.CreditCard.Database;
using Dapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.CreditCard.CreditCard
{

    public interface ICreditCardRepository
    {
        public Task<IEnumerable<CreditCardDto>> GetAll();
        public Task<CreditCardDto> GetByQuery(string name, string creditCardNumber);
        public Task<Guid> Insert(CreditCardDto creditCardDto);

    }

    public class CreditCardRepository : ICreditCardRepository, IDisposable
    {
        private bool disposedValue;

        private ISqlDbConnection _sqlDbConnection { get; set; }
        public CreditCardRepository(ISqlDbConnection sqlDbConnection)
        {
            _sqlDbConnection = sqlDbConnection;
        }

        public async Task<IEnumerable<CreditCardDto>> GetAll()
        {
            var connection = await _sqlDbConnection.OpenConnectionAsync();

            return await connection.QueryAsync<CreditCardDto>("[dbo].GetCreditCardDetails", transaction: _sqlDbConnection.SqlTransaction, commandType: CommandType.StoredProcedure);
        }

        public async Task<CreditCardDto> GetByQuery(string name = null, string creditCardNumber = null)
        {
            var connection = await _sqlDbConnection.OpenConnectionAsync();

            var parameters = new { 
                                    Name = name,
                                    CardNumber = creditCardNumber
                                 };

            return (await connection.QueryAsync<CreditCardDto>("[dbo].GetCreditCardDetails", param: parameters, transaction: _sqlDbConnection.SqlTransaction, commandType: CommandType.StoredProcedure)).SingleOrDefault();
        }

        public async Task<Guid> Insert(CreditCardDto creditCardDto)
        {
            var connection = await _sqlDbConnection.OpenConnectionAsync();

            var parameters = new
            {
                Name = creditCardDto.Name,
                CardNumber = creditCardDto.CardNumber,
                CVC = creditCardDto.CVC,
                ExpiryDate = creditCardDto.ExpiryDate

            };

            return (await connection.QueryAsync<Guid>("[dbo].InsertCreditCard", param: parameters, transaction: _sqlDbConnection.SqlTransaction, commandType: CommandType.StoredProcedure)).SingleOrDefault();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_sqlDbConnection != null)
                        _sqlDbConnection.Dispose();
                }
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
