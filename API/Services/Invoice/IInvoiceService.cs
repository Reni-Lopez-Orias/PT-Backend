using API.Models;

namespace API.Services.Invoices
{
    public interface IInvoiceService
    {
        ResponseBase<bool> CreateInvoice(Invoice request);
        ResponseBase<List<Invoice>> GetInvoces();
    }
}
