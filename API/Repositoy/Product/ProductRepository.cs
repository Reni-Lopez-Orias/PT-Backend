using API.Repository.DBContext;
using Dapper;
using Serilog;
using System.Data;
using API.Models;

namespace API.Repositoy.ProductRepo
{
    public class ProductRepository : IProductRepository
    {

        private readonly IDbContext _dbContext;

        public ProductRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string CreateProduct(Product product)
        {
            string response = "";

            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var procedure = "CreateProduct";
                    var parameters = new DynamicParameters();
                    parameters.Add("@prmTax", product.Tax, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@prmName", product.Name, DbType.String, ParameterDirection.Input, 100);
                    parameters.Add("@prmDetails", product.Details, DbType.String, ParameterDirection.Input, 250);
                    parameters.Add("@prmCode", product.Code, DbType.String, ParameterDirection.Input, 250);
                    parameters.Add("@prmPrice", product.Price, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@prmMessageResponse", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
                    parameters.Add("@prmIsValid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);

                    var isvalid = parameters.Get<int>("@prmIsValid");

                    if (isvalid == 0)
                    {
                        response = parameters.Get<string>("@prmMessageResponse");
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "CreateProduct");
                throw new Exception("Ha ocurrido un error interno!");
            }

            return response;
        }

        public string EditProduct(Product product)
        {
            string response = "";

            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var procedure = "EditProduct";
                    var parameters = new DynamicParameters();
                    parameters.Add("@prmIdProduct", product.IdProduct, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@prmTax", product.Tax, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@prmName", product.Name, DbType.String, ParameterDirection.Input, 100);
                    parameters.Add("@prmDetails", product.Details, DbType.String, ParameterDirection.Input, 250);
                    parameters.Add("@prmCode", product.Code, DbType.String, ParameterDirection.Input, 250);
                    parameters.Add("@prmPrice", product.Price, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@prmMessageResponse", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
                    parameters.Add("@prmIsValid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);

                    var isvalid = parameters.Get<int>("@prmIsValid");

                    if (isvalid == 0)
                    {
                        response = parameters.Get<string>("@prmMessageResponse");
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "EditProduct");
                throw new Exception("Ha ocurrido un error interno!");
            }

            return response;
        }

        public Product GetProductByCode(string code)
        {
            Product response = new Product();

            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var procedure = "GetProductByCode";
                    var parameters = new DynamicParameters();
                    parameters.Add("@prmCode", code, DbType.String, ParameterDirection.Input, 250);

                    var product = connection.Query<Product>(procedure, parameters, commandType: CommandType.StoredProcedure).First();

                    if (product != null)
                    {
                        response = product;
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Source == "System.Linq")
                {
                    throw new Exception("No existe!");
                }
                Log.Error(ex, "GetProductByCode");
                throw new Exception("Ha ocurrido un error interno!");
            }

            return response;
        }

        public List<Product> GetProducts()
        {
            List<Product> response = new List<Product>();

            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var procedure = "GetProducts";

                    var productList = connection.Query<Product>(procedure, commandType: CommandType.StoredProcedure).AsList();

                    if (productList.Count > 0)
                    {
                        response = productList;
                    }
                    else
                    {
                        throw new Exception("No existen registros!");
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Source == "API")
                {
                    throw new Exception(ex.Message);
                }
                Log.Error(ex, "GetProducts");
                throw new Exception("Ha ocurrido un error interno!");
            }

            return response;
        }

        public string DeleteProduct(string code)
        {
            string response = "";

            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var procedure = "DeleteProduct";

                    var parameters = new DynamicParameters();
                    parameters.Add("@prmCode", code, dbType: DbType.String, direction: ParameterDirection.Input, 255);
                    parameters.Add("@prmMessageResponse", size: 255, dbType: DbType.String, direction: ParameterDirection.Output);
                    parameters.Add("@prmIsValid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);

                    var isvalid = parameters.Get<int>("@prmIsValid");

                    if (isvalid == 0)
                    {
                        response = parameters.Get<string>("@prmMessageResponse");
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "DeleteProduct");
                throw new Exception("Ha ocurrido un error interno!");
            }

            return response;
        }

        public List<Product> GetProductInvoice(int idInvoice)
        {
            List<Product> response = new List<Product>();

            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var procedure = "GetProductInvoice";
                    var parameters = new DynamicParameters();
                    parameters.Add("@prmIdInvoice", idInvoice, DbType.Int32, ParameterDirection.Input);
                    var productList = connection.Query<Product>(procedure, parameters, commandType: CommandType.StoredProcedure).AsList();

                    if (productList.Count > 0)
                    {
                        response = productList;
                    }
                    else
                    {
                        throw new Exception("No existen registros!");
                    }

                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, "GetProductInvoice");
                throw new Exception("Ha ocurrido un error interno!");
            }

            return response;

        }

    }
}
