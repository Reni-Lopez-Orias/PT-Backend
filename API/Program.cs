using API.Repository.DBContext;
using API.Repositoy.Auth;
using API.Repositoy.InvoicesRepo;
using API.Repositoy.ProductRepo;
using API.Services.Auth;
using API.Services.Invoices;
using API.Services.password;
using API.Services.Products;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddControllers();

builder.Services.AddSingleton(configuration);  // Registrar IConfiguration para DatabaseContext

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IDbContext, DbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                      });
});

var app = builder.Build();

// Configurar el pipeline de la solicitud HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();

app.Run();
