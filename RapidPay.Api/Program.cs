using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RapidPay.Api.ConfigExtensions;
using RapidPay.Api.Middlewares;
using RapidPay.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

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

builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.DefaultModelsExpandDepth(-1);
	});
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler();

app.Run();
