using API.Models;

namespace API.Repositoy.InvoicesRepo
{
    public interface IInvoiceRepository
    {
        int CreateInvoice(Invoice invoice);
        bool CreateDetailsInvoice(Invoice invoice);
        List<Invoice> GetInvoces();
    }
}
