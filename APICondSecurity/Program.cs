using APICondSecurity;
using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Repositories;
using APICondSecurity.Services;
using APICondSecurity.Infra.Ioc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactNativeApp",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddDbContext<condSecurityContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureSwagger();

builder.Services.AddAutoMapper(typeof(EntitiesToDTOMappingProfile));
// builder.Services.AddScoped<IAuthenticate, AuthenticateService>();
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
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
builder.Services.AddScoped<UfRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<VeiculoRepository>();
builder.Services.AddScoped<VeiculoUsuarioRepository>();
builder.Services.AddScoped<VeiculoTerceiroRepository>();
builder.Services.AddSingleton<ITemporaryStorageService, TemporaryStorageService>();



var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowReactNativeApp");

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/notificationHub");

app.Run();


