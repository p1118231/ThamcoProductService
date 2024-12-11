using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using ThamcoProducts.Services.Undercutters;
using ThamcoProducts.Services.ProductRespository;
using Polly;
using Polly.Extensions.Http;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();  // Register HttpClient for DI

builder.Services.AddScoped<IUndercuttersService, UndercuttersService>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = builder.Configuration["Auth:Domain"];
                        options.Audience = builder.Configuration["Auth:Audience"];
                    });
builder.Services.AddAuthorization();
builder.Logging.AddConsole();

if(builder.Environment.IsDevelopment()){
   // builder.Services.AddSingleton<IUndercuttersService, UndercuttersFakeService>();
    builder.Services.AddSingleton<IProductSerivce, ProductFakeService>();
}
else {

   builder.Services.AddHttpClient<IUndercuttersService, UndercuttersService>()
                    .AddPolicyHandler(GetRetryPolicy())
                    .AddPolicyHandler(GetCircuitBreakerPolicy()); ;
    
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(5, retryAttempt =>
            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}

IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
}

