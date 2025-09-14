using Domain.Entity;

namespace Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<Invoice?> GetByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<int> AddAsync(Invoice invoice);
    }
}
