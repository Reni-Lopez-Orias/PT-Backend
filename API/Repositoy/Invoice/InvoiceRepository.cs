using API.Models;
using API.Repository.DBContext;
using Dapper;
using Serilog;
using System.Data;
namespace API.Repositoy.InvoicesRepo
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IDbContext _dbContext;

        public InvoiceRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        int IInvoiceRepository.CreateInvoice(Invoice invoice)
        {
            int idInvoice = 0;
            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var procedure = "CreateInvoice";
                    var parameters = new DynamicParameters();
                    parameters.Add("@prmIdUser", invoice.IdUser, dbType: DbType.Int32, ParameterDirection.Input, 250);
                    parameters.Add("@prmClientName", invoice.ClientName, DbType.String, ParameterDirection.Input, 255);
                    parameters.Add("@prmRegisterDate", invoice.RegisterDate, DbType.String, ParameterDirection.Input, 255);
                    parameters.Add("@prmIdInvoice", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);

                    var isvalid = parameters.Get<int>("@prmIdInvoice");

                    if (isvalid != 0)
                    {
                        idInvoice = isvalid;
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "CreatInvoice");
                throw new Exception("Ha ocurrido un error interno!");
            }
            return idInvoice;
        }

        bool IInvoiceRepository.CreateDetailsInvoice(Invoice invoice)
        {
            var createdDetailInvoice = false;

            try
            {

                foreach (var item in invoice.ProductsInvoice)
                {
                    using (var connection = _dbContext.Connection)
                    {
                        var procedure = "CreateDetailsInvoice";
                        var parameters = new DynamicParameters();
                        parameters.Add("@prmIdInvoice", invoice.IdInvoice, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@prmIdProduct", item.IdProduct, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@prmAmount", item.Amount, DbType.Decimal, ParameterDirection.Input);
                        parameters.Add("@prmTotalTax", item.TotalTax, DbType.Decimal, ParameterDirection.Input);
                        parameters.Add("@prmSubTotal", item.SubTotal, DbType.Decimal, ParameterDirection.Input);
                        parameters.Add("@prmTotal", item.TotalPrice, DbType.Decimal, ParameterDirection.Input);
                        parameters.Add("@prmIsValid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);

                        var isvalid = parameters.Get<int>("@prmIsValid");

                        if (isvalid != 0)
                        {
                            createdDetailInvoice = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "CreatInvoiceDetails");
                throw new Exception("Ha ocurrido un error interno!");
            }
            return createdDetailInvoice;
        }

        public List<Invoice> GetInvoces()
        {
            List<Invoice> response = new List<Invoice>();

            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var procedure = "GetInvoices";
                    var invoices = connection.Query<Invoice>(procedure, commandType: CommandType.StoredProcedure).ToList();

                    if (invoices != null && invoices.Any())
                    {
                        response = invoices;
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "CreatInvoice");
                throw new Exception("Ha ocurrido un error interno!");
            }

            return response;

        }
    }

}
