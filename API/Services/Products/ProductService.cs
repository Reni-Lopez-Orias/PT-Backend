using API.Models;
using API.Repositoy.Auth;
using API.Repositoy.ProductRepo;
using Azure.Core;

namespace API.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ResponseBase<Product> CreateProduct(Product product)
        {
            ResponseBase<Product> response = new ResponseBase<Product>();
            try
            {
                string responseRegisterProduct = _productRepository.CreateProduct(product);

                if (responseRegisterProduct == "")
                {
                    Product productByCode = _productRepository.GetProductByCode(product.Code);
                    if (productByCode != null)
                    {
                        response.Response = productByCode;
                        response.Message = "Agregado con exito";
                    }
                    else
                    {
                        response.Response = null;
                        response.Message = "Ya existe";
                    }
                    response.Error = false;

                }
                else
                {
                    response.Error = true;
                    response.Message = responseRegisterProduct;
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

        public ResponseBase<Product> DeleteProduct(string code)
        {
            ResponseBase<Product> response = new ResponseBase<Product>();
            try
            {
                Product productByCodeResponse = _productRepository.GetProductByCode(code);
                if (productByCodeResponse != null)
                {
                    string deleteResponse = _productRepository.DeleteProduct(code);
                    if (deleteResponse == "")
                    {
                        response.Response = null;
                        response.Message = "Producto eliminado!";
                    }
                    else
                    {
                        response.Response = null;
                        response.Message = deleteResponse;
                    }
                    response.Error = false;
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

        public ResponseBase<Product> EditProduct(Product product)
        {
            ResponseBase<Product> response = new ResponseBase<Product>();
            try
            {
                string responseRegisterProduct = _productRepository.EditProduct(product);
                if (responseRegisterProduct == "")
                {
                    Product productByCode = _productRepository.GetProductByCode(product.Code);
                    if (productByCode != null)
                    {
                        response.Response = productByCode;
                        response.Message = "Producto editado!";
                    }
                    else
                    {
                        response.Response = null;
                        response.Message = responseRegisterProduct;
                    }
                    response.Error = false;
                    

                }
                else
                {
                    response.Error = true;
                    response.Message = responseRegisterProduct;
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

        public ResponseBase<Product> GetProductByCode(string code)
        {
            ResponseBase<Product> response = new ResponseBase<Product>();
            try
            {
                Product productByCode = _productRepository.GetProductByCode(code);

                if (productByCode != null)
                {
                    response.Error = true;
                    response.Message = "";
                    response.Response = productByCode;
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

        public ResponseBase<List<Product>> GetProducts()
        {
            ResponseBase<List<Product>> response = new ResponseBase<List<Product>>();
            try
            {
                List<Product> responseRegisterProduct = _productRepository.GetProducts();
                response.Error = true;
                response.Message = "";
                response.Response = responseRegisterProduct;
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
