namespace API.Models
{
    public class Invoice
    {
        public int IdInvoice { get; set; }
        public int IdUser { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string RegisterDate { get; set; } = string.Empty;
        public List<Product> ProductsInvoice { get; set; }

    }
}
