using API.Models;

namespace API.Services.Products
{
    public interface IProductService
    {
        ResponseBase<Product> CreateProduct(Product product);
        ResponseBase<Product> EditProduct(Product product);
        ResponseBase<Product> GetProductByCode(string code);
        ResponseBase<List<Product>> GetProducts();
        ResponseBase<Product> DeleteProduct(string code);
    }
}
