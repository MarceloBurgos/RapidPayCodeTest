using Microsoft.AspNetCore.Mvc;
using RapidPay.Api.ConfigExtensions;
using RapidPay.Api.Middlewares;
using RapidPay.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddMvc(options =>
{
	options.Filters.Add(new ProducesAttribute("application/json"));
}).AddJsonOptions(options =>
{
	JsonSerializerGlobalOptions.SetGlobalOptions(options.JsonSerializerOptions);
});

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
app.UseExceptionHandler();

app.Run();
