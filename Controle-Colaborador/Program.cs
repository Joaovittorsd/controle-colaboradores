using ControleColaborador.API.Endpoints;
using ControleColaborador.Shared.Dados.Dados;
using ControleColaborador.Shared.Modelos.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ControleColaboradoresDB");

builder.Services.AddDbContext<ControleColaboradorContext>(options =>
{
    options
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .UseLazyLoadingProxies();
});
builder.Services.AddTransient<DAL<Colaborador>>();
builder.Services.AddTransient<DAL<Cargo>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddCors();
var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.AddEndPointsColaboradores();
app.AddEndPointsCargos();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
