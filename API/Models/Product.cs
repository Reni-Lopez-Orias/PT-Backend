namespace API.Models
{
    public class Product
    {
        public int? IdProduct { get; set; }
        public int Tax { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? SubTotal { get; set;}
        public decimal? Amount { get; set; }
        public decimal? TotalInvoice { get; set; }
        public decimal? TotalTax { get; set; }
    }
}
