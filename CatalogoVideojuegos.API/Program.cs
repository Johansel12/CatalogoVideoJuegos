using CatalogoVideojuegos.API.Data;
using CatalogoVideojuegos.API.Middleware;
using CatalogoVideojuegos.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Controladores de la API
builder.Services.AddControllers();

// Repositorio genérico (persistencia en archivos JSON)
builder.Services.AddSingleton(typeof(IRepositorio<>), typeof(RepositorioJson<>));

// Servicios de cada módulo
// (cada rama feature/ registra aquí su servicio)
builder.Services.AddScoped<IDesarrolladoraService, DesarrolladoraService>();
builder.Services.AddScoped<IPlataformaService, PlataformaService>();
builder.Services.AddScoped<IVideojuegoService, VideojuegoService>();
builder.Services.AddScoped<IResenaService, ResenaService>();

var app = builder.Build();

app.UseMiddleware<ManejoErroresMiddleware>();

// Interfaz web ubicada en wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();
