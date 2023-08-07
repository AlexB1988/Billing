using Billing.Application.Interfaces;
using Billing.Application.Services;
using Billing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBalance, BalanceFormFile>();
builder.Services.AddScoped<IPayment, PaymentFromFile>();
builder.Services.AddScoped<IGetBalancesService, GetBalancesService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
