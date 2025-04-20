using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestApiSep.Automappers;
using TestApiSep.DTOs;
using TestApiSep.Repository;
using TestApiSep.Service;
using TestApiSep.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Services
builder.Services.AddScoped<ICedulaService, CedulaService>();

// Repository
builder.Services.AddScoped<IRepository<RegistroDto>, ParticipanteRepository>();

//Entity Framework
builder.Services.AddDbContext<DBTestSepContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});

//Validators
builder.Services.AddScoped<IValidator<RegistroInsertDto>, RegistroInsertValidator>();

//HttpClient
builder.Services.AddHttpClient("ApiSep", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["UrlSep"]);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

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
