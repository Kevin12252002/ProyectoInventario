using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using ProyectoInventario.Services;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //Este servicio es el que se encarga que haga los servicios de las vistas

builder.Services.AddDbContext<LibreriaContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IServicioListaP, ServicioListaP>();
builder.Services.AddScoped<IServicioProducto, ServicioProducto>();
builder.Services.AddScoped<IServicioUsuario, ServicioUsuario>();
builder.Services.AddScoped<IServicioImagen, ServicioImagen>();
builder.Services.AddScoped<IServicioCategoria, ServicioCategoria>();
builder.Services.AddScoped<IServicioBodega, ServicioBodega>();
builder.Services.AddScoped<IServicioImagenP, ServicioImagenP>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = "/Login/IniciarSesion";
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
             });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=IniciarSesion}/{id?}");

app.Run();
