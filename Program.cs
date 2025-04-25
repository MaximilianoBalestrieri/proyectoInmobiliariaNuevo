using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using proyectoInmobiliariaNuevo.Models; // Asegurate de incluir el namespace donde está ConexionDB

var builder = WebApplication.CreateBuilder(args);

// Agregamos servicios
builder.Services.AddControllersWithViews();

// Registramos IHttpContextAccessor para poder usarlo en las vistas
builder.Services.AddHttpContextAccessor();

// 🫶 Registramos el sistema de sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 💘 Registramos ConexionDB como servicio con su cadena de conexión
string cadenaConexion = builder.Configuration.GetConnectionString("MiConexion");
builder.Services.AddScoped<ConexionDB>(provider => new ConexionDB(cadenaConexion));

// 🔧 Construimos la aplicación
var app = builder.Build();

// Configuración del pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // No olvidarse de esto para CSS/JS

app.UseRouting();

app.UseSession(); // 🧠 Importantísimo, activa las sesiones
app.UseAuthorization();

// 💡 Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
