global using backend.Models;
global using backend.Entities;

using backend.Data;
using backend.Services;
using backend.Services.StoreServices;
using backend.Services.AuthServices;
using backend.Services.ProductServices;
using backend.Services.CategoryServices;
using backend.Services.CloudinaryServices;
using backend.Services.QrCodeServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using backend.Filters;
using Serilog;
using Serilog.Events; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error() // μόνο Error και πιο πάνω (Fatal) θα γραφτούν
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "MyApp")
    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error) // μόνο για κονσόλα
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true
        },
        restrictedToMinimumLevel: LogEventLevel.Error // ΜΟΝΟ errors στη βάση
    )
    .CreateLogger();

builder.Host.UseSerilog();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.MapType<IFormFile>(() =>
     new OpenApiSchema { Type = "string", Format = "binary" }
 );
    options.MapType<List<IFormFile>>(() =>
        new OpenApiSchema
        {
            Type = "array",
            Items = new OpenApiSchema { Type = "string", Format = "binary" }
        }
    );

    // —— Ενεργοποίηση του OperationFilter για multipart/form-data ——
    options.OperationFilter<FileUploadOperationFilter>();
});
builder.Services.AddCors(options =>
{
    //options.AddDefaultPolicy(policy =>
    //{
    //    policy
    //        .WithOrigins("http://localhost:5173", "https://yourfrontend.azurestaticapps.net")
    //        .AllowAnyHeader()
    //        .AllowAnyMethod();
    //});
    //options.AddPolicy("AllowFrontend", policy =>
    //{
    //    policy
    //        .WithOrigins("https://proud-field-0d0b23f03-preview.westeurope.6.azurestaticapps.net",
    //        "http://localhost:5173"
    //        )
    //        .AllowAnyHeader()
    //        .AllowAnyMethod();
    //});

    options.AddPolicy("AllowAll",
       b => b.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

});

builder.WebHost.ConfigureKestrel(options =>
{
    // HTTP
    options.ListenAnyIP(5172);

    // HTTPS
    options.ListenAnyIP(7176, listenOptions =>
    {
        listenOptions.UseHttps(); // μόνο αν έχεις dev cert
    });
});
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IQrCodeService, QrCodeService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();
    var controller = endpoint?.Metadata.GetMetadata<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>()?.ControllerName;
    var action = endpoint?.Metadata.GetMetadata<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>()?.ActionName;

    using (Serilog.Context.LogContext.PushProperty("Controller", controller))
    using (Serilog.Context.LogContext.PushProperty("Action", action))
    {
        await next();
    }
});


app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();