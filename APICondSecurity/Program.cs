using APICondSecurity;
using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Repositories;
using APICondSecurity.Services;
using APICondSecurity.Infra.Ioc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Configuração do serviço de configuração
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Configuração de CORS para permitir o acesso do aplicativo React Native
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactNativeApp",
        builder => builder
            .WithOrigins("http://localhost:8081/", "https://czyhr5k-jussan-8081.exp.direct/", "https://condsecuritysignalr.service.signalr.net")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Adição de serviços ao contêiner
builder.Services.AddDbContext<condSecurityContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddHttpClient();

var signalRConnectionString = builder.Configuration.GetConnectionString("AzureSignalR");
builder.Services.AddSignalR().AddAzureSignalR(signalRConnectionString);

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureSwagger();

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(EntitiesToDTOMappingProfile));

// Registro de serviços e repositórios no contêiner de dependências
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

// Configuração do pipeline de requisições HTTP
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowReactNativeApp");
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
