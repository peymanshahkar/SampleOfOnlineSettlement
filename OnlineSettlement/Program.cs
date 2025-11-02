using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Data.SqlClient;
using System.Data;
using OnlineSettlement.Data;
using Microsoft.Extensions.Http;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();


var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddEndpointsApiExplorer();

// افزودن Swagger به DI container  
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineSettlement API", Version = "v1" });
});



builder.Services.AddScoped<IDbConnection>((sp) =>
    new SqlConnection(configuration.GetConnectionString("DefaultConnection")));


var connectionString = configuration.GetConnectionString("DefaultConnection");


builder.Services.AddScoped<ZarinPalService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>(_ => new PaymentRepository(connectionString));
builder.Services.AddScoped<IDebitRepository, DebitRepository>(_ => new DebitRepository(connectionString));
builder.Services.AddScoped<ZarinPalService, ZarinPalService>();


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();

    app.UseSwagger();
   // app.UseSwaggerUI();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineSettlement API V1");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();
