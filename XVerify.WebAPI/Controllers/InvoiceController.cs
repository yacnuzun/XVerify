using Application.Interfaces;
using Application.Services.Implamentations;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace XVerify.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _invoiceService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Invoice invoice)
        {
            var id = await _invoiceService.AddAsync(invoice);
            invoice.Id = id;
            return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
        }

    }

}
