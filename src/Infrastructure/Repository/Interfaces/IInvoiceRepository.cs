using Domain.Entity;

namespace Infrastructure.Repository.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice?> GetByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<int> AddAsync(Invoice invoice);
    }

}
