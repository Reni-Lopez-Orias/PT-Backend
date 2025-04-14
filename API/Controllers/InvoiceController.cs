using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Services.Invoices;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService iInvoiceService)
        {
            _invoiceService = iInvoiceService;
        }

        [HttpPost("CreateInvoice")]
        public ResponseBase<bool> CreateInvoice(Invoice request)
        {
            return _invoiceService.CreateInvoice(request);
        }

        [HttpGet("GetInvoces")]
        public ResponseBase<List<Invoice>> GetInvoces()
        {
            return _invoiceService.GetInvoces();
        }
    }
}
