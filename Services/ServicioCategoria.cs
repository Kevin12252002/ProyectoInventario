using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using ProyectoInventario.Models.Entidades;

namespace ProyectoInventario.Services
{
    public class ServicioCategoria : IServicioCategoria
    {
        private readonly LibreriaContext _context;

        public ServicioCategoria(LibreriaContext context)
        {
            _context = context;

        }

        public async Task<Categoria> GetCategoria(string NombreCategorias, string Descripcion, string TipoProducto)
        {
            Categoria categoria = await _context.Categorias.Where(u => u.NombreCategorias == NombreCategorias && u.Descripcion == Descripcion && u.TipoProducto == TipoProducto).FirstOrDefaultAsync();

            return categoria;
        }

        public async Task<Categoria> GetCategoria(string NombreCategoria)
        {
            return await _context.Categorias.FirstOrDefaultAsync(u => u.NombreCategorias == NombreCategoria);
        }

       

        public async Task<Categoria> SaveCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        Task<Producto> IServicioCategoria.GetCategoria(string NombreCategoria, string Descripcion, string TipoProducto)
        {
            throw new NotImplementedException();
        }

        Task<Producto> IServicioCategoria.GetCategoria(string NombreCategoria)
        {
            throw new NotImplementedException();
        }

        Task<Producto> IServicioCategoria.SaveCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }
    }
}
