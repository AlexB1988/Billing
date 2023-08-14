using Billing.Application.DTOs;
using Billing.Application.Formatters;
using Billing.Application.Interfaces;
using Billing.Application.Repositories;
using Billing.Application.Services;
using Billing.Infrastructure;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBalance, BalanceFormFile>();
builder.Services.AddScoped<IPayment, PaymentFromFile>();
builder.Services.AddScoped<IGetBalancesService, GetBalancesService>();
builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();
builder.Services.AddScoped<IBalancesPerMonth, BalancesPerMonth>();
builder.Services.AddScoped<IValidator<GetBalancesParametersDto>, GetBalancesParametersDtoValidator>();

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
}).AddXmlSerializerFormatters()
  .AddMvcOptions(options => options.OutputFormatters.Add(new CsvOutputFormatter()));

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
