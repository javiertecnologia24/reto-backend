using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pedidos.API.Middlewares;
using Pedidos.Application.Features.Pedidos.Commands.CreatePedido;
using Pedidos.Application.Features.Pedidos.Commands.DeletePedido;
using Pedidos.Application.Features.Pedidos.Commands.UpdatePedido;
using Pedidos.Application.Features.Pedidos.Queries.GetAllPedidos;
using Pedidos.Application.Features.Pedidos.Queries.GetPedidoById;
using Pedidos.Application.Interfaces;
using Pedidos.Domain.Interfaces;
using Pedidos.Infrastructure.Data;
using Pedidos.Infrastructure.Repositories;
using Pedidos.Infrastructure.Security;
using Pedidos.Infrastructure.ServicesExternal;
using Serilog;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//Dependencias
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(GetAllPedidosQuery).Assembly,
        typeof(GetPedidoByIdQuery).Assembly,
        typeof(CreatePedidoCommand).Assembly,
        typeof(DeletePedidoCommand).Assembly,
        typeof(UpdatePedidoCommand).Assembly
    );
});
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ExternalService>();
builder.Services.AddSingleton<HttpClient>();


//log
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console();
});


//seguridad
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["key"]);

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddAuthorization();


//coors
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirLocalhost", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
        origin.StartsWith("http://localhost") || origin.StartsWith("https://localhost"))
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirLocalhost");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
