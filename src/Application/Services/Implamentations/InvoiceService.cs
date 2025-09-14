using Application.Interfaces;
using Domain.Entity;
using Infrastructure.Repository.Interfaces;
using Infrastructure.UnitofWork;

namespace Application.Services.Implamentations
{
    public class InvoiceService:IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            _unitOfWork.Begin();
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            _unitOfWork.Commit();
            return invoice;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            _unitOfWork.Begin();
            var invoices = await _invoiceRepository.GetAllAsync();
            _unitOfWork.Commit();
            return invoices;
        }

        public async Task<int> AddAsync(Invoice invoice)
        {
            _unitOfWork.Begin();
            var id = await _invoiceRepository.AddAsync(invoice);
            _unitOfWork.Commit();
            return id;
        }
    }

}
