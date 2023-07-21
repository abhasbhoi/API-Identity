using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using APIIdentity.BusinessLayers;
using APIIdentity.BusinessLayers.Contracts;
using APIIdentity.Models;
using APIIdentity.Repository;
using APIIdentity.Repository.Contract;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using APIIdentity.Authentication;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var MyAllowSpecificOrigins = "_apiBaseAllowSpecificOrigins";
builder.Services.AddCors(option => option.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    }));

// add services related to controller and HTTP requests
builder.Services.AddControllers();

//builder.Services.AddDbContext<MyAppDBContext>(opt => opt.UseInMemoryDatabase("Employees"));

// EF DB Connection
builder.Services.AddDbContext<MyAppDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MyAppDBContext>()
                .AddDefaultTokenProviders(); ;


//Register instances
builder.Services.AddScoped<IEmployeeBusinessLayer, EmployeeBusinessLayer>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Identity V1"
    });
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "API Identity V2"
    });

    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1.0");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2.0");
    });
}

app.UseCors(MyAllowSpecificOrigins);

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.Run();

