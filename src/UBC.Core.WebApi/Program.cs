using AutoMapper;
using UBC.Core.Data.Mappers;
using UBC.Core.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .SetBasePath(builder.Environment.ContentRootPath)
       .AddJsonFile("appsettings.json", true, true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
       .AddEnvironmentVariables();

#region ConfigureServices

builder.Services.AddApiCommonsConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration(builder.Configuration);

// DependencyInjectionConfig
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);

//// Inicio AutoMapper
builder.Services.AddAutoMapper(typeof(StudentProfile));//UserIdentityProfile

#region Databases Configurations

builder.Services.AddDatabaseConfiguration(builder.Configuration);

//// Identity Configuration Extension Method and DbConnection
//builder.Services.AddIdentityConfiguration(builder.Configuration);

#endregion

#endregion

#region Configure

await using var app = builder.Build();
var loggerFactory = app.Services.GetService<ILoggerFactory>();

app.UseSwaggerConfiguration();

app.UseApiCommonsConfiguration(app.Environment, loggerFactory);

await app.RunAsync();

#endregion

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
