using FluentValidation.AspNetCore;
using Microservice.Account.API.SeedWork.ProblemDetails;
using Microservice.Account.Application;
using Microservice.Account.EFCore;
using Microsoft.OpenApi.Models;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehaior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region [ Registeration ]
builder.Services.AddApplication();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddExceptionHandler<FluentValidatorExceptionHandler>();


#endregion

#region [ PostgreSQL Configuration ]
builder.Services.AddInfrastructureEFCore(builder.Configuration);
#endregion

#region [ Swagger Configuration ]
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Account API", Version = "v1" });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
});

app.Run();