using API.Models;
using API.Repository.DBContext;
using API.Repositoy.InvoicesRepo;
using API.Repositoy.ProductRepo;

namespace API.Services.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository, IProductRepository productRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
        }

        public ResponseBase<bool> CreateInvoice(Invoice request)
        {
            ResponseBase<bool> response = new ResponseBase<bool>();

            try
            {
                var createdInvoice = _invoiceRepository.CreateInvoice(request);

                if (createdInvoice != 0)
                {
                    request.IdInvoice = createdInvoice;
                    var createdDetailsInvoice = _invoiceRepository.CreateDetailsInvoice(request);

                    if (createdDetailsInvoice)
                    {
                        response.Error = false;
                        response.Message = "Compra realizada correctamente!";
                        response.Response = false;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Ha ocurrido un error a generar la factura!";
                        response.Response = false;
                    }
                }

            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                response.Response = false;
            }

            return response;
        }

        public ResponseBase<List<Invoice>> GetInvoces()
        {
            ResponseBase<List<Invoice>> response = new ResponseBase<List<Invoice>>();

            try
            {

                var invoice = _invoiceRepository.GetInvoces();

                if(invoice != null)
                {
                    foreach (var item in invoice)
                    {
                        var productsInvoce = _productRepository.GetProductInvoice(item.IdInvoice);

                        if (productsInvoce != null)
                        {
                            item.ProductsInvoice = productsInvoce;
                        }
                    }

                    response.Error = false;
                    response.Message = "";
                    response.Response = invoice;

                }
                else
                {
                    response.Error = true;
                    response.Message = "No existe";
                    response.Response = null;
                }

            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                response.Response = null;
            }

            return response;
        }
    }

}
