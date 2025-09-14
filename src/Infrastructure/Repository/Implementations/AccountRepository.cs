using Dapper;
using Domain.Entity;
using Infrastructure.Repository.Interfaces;
using Infrastructure.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Implementations
{
    public class AccountRepository:IAccountRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Accounts WHERE Id = @Id";
            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Account>(sql, new { Id = id }, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            var sql = "SELECT * FROM Accounts";
            return await _unitOfWork.Connection.QueryAsync<Account>(sql, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> AddAsync(Account account)
        {
            var sql = "INSERT INTO Accounts (Username, PasswordHash, Email) VALUES (@Username, @PasswordHash, @Email) RETURNING Id;";
            return await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, account, _unitOfWork.Transaction);
        }

    }
}
