using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using ProyectoInventario.Models.Entidades;

namespace ProyectoInventario.Services
{
    public class ServicioProducto: IServicioProducto
    {
        private readonly LibreriaContext _context;

        public ServicioProducto(LibreriaContext context)
        {
            _context = context;

        }

        public async Task<Producto> GetProducto(string Nombre, decimal Precio, int Stock, string URLFotoProducto, bool Activo, string Marca, string Modelo)
        {
            Producto producto = await _context.Productos.Where(u => u.NombreProducto == Nombre && u.Precio == Precio && u.Stock==Stock && u.URLFotoProducto== URLFotoProducto && u.Activo==Activo 
            && u.Marca==Marca && u.Modelo==Modelo).FirstOrDefaultAsync();

            return producto;
        }

        public async Task<Producto> GetProducto(string Nombre)
        {
            return await _context.Productos.FirstOrDefaultAsync(u => u.NombreProducto == Nombre);
        }

        public async Task<Producto> SaveProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

    }
}

    
