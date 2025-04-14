using API.Models;

namespace API.Repositoy.ProductRepo
{
    public interface IProductRepository
    {
        string CreateProduct(Product product);
        string EditProduct(Product product);
        Product GetProductByCode(string code);
        List<Product> GetProducts(); 
        List<Product> GetProductInvoice(int idInvoice); 
        string DeleteProduct(string code);
    }
}
