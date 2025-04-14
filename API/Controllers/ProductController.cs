using API.Models;
using API.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("CreateProduct")]
        public ResponseBase<Product> CreateProduct(Product request)
        {
            return _productService.CreateProduct(request);
        }

        [HttpPost("EditProduct")]
        public ResponseBase<Product> EditProduct(Product request)
        {
            return _productService.EditProduct(request);
        }

        [HttpGet("GetProductByCode")]
        public ResponseBase<Product> GetProductByCode(string code)
        {
            return _productService.GetProductByCode(code);
        }

        [HttpGet("GetProducts")]
        public ResponseBase<List<Product>> GetProducts()
        {
            return _productService.GetProducts();
        }

        [HttpDelete("DeleteProduct")]
        public ResponseBase<Product> DeleteProduct(string code)
        {
            return _productService.DeleteProduct(code);
        }

    }
}
