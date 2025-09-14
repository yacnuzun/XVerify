using Dapper;
using Domain.Entity;
using Infrastructure.Repository.Interfaces;
using Infrastructure.UnitofWork;

namespace Infrastructure.Repository.Implementations
{
    public class InvoiceRepository:IInvoiceRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Invoices WHERE Id = @Id";
            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Invoice>(sql, new { Id = id }, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            var sql = "SELECT * FROM Invoices";
            return await _unitOfWork.Connection.QueryAsync<Invoice>(sql, transaction: _unitOfWork.Transaction);
        }

        public async Task<int> AddAsync(Invoice invoice)
        {
            var sql = "INSERT INTO Invoices (AccountId, InvoiceNumber, Amount, CreatedAt) VALUES (@AccountId, @InvoiceNumber, @Amount, @CreatedAt) RETURNING Id;";
            return await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, invoice, _unitOfWork.Transaction);
        }

    }
}
