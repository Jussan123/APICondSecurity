using APICondSecurity.Interfaces;
using APICondSecurity.Mappings;
using APICondSecurity.Models;
using APICondSecurity.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


// Add services to the container.

builder.Services.AddDbContext<condSecurityContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CameraRepository>();
builder.Services.AddScoped<CidadeRepository>();
builder.Services.AddScoped<CondominioRepository>();
builder.Services.AddScoped<EnderecoRepository>();
builder.Services.AddScoped<NotificacaoRepository>();
builder.Services.AddScoped<PermissaoRepository>();
builder.Services.AddScoped<PortaoRepository>();
builder.Services.AddScoped<RegistrosRepository>();
builder.Services.AddScoped<ResidenciaRepository>();
builder.Services.AddScoped<RfidRepository>();
builder.Services.AddScoped<TipoUsuarioRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<VeiculoRepository>();
//Adicionar AutoMapper para o veiculo
builder.Services.AddAutoMapper(typeof(EntitiesToDTOMappingProfile));
builder.Services.AddScoped<VeiculoUsuarioRepository>();

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


