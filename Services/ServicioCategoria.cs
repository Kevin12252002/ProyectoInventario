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

        public async Task<Categoria> GetCategoria(string NombreC, string Descripcion, string TipoProducto)
        {
            Categoria categoria = await _context.Categorias.Where(u => u.NombreCategoria == NombreC && u.Descripcion == Descripcion && u.TipoProducto == TipoProducto).FirstOrDefaultAsync();

            return categoria;
        }

        public async Task<Categoria> GetCategoria(string NombreC)
        {
            return await _context.Categorias.FirstOrDefaultAsync(u => u.NombreCategoria == NombreC);
        }

        public async Task<Categoria> SaveCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        Task<Producto> IServicioCategoria.GetCategoria(string NombreC, string Descripcion, string TipoProducto)
        {
            throw new NotImplementedException();
        }

        Task<Producto> IServicioCategoria.GetCategoria(string NombreC)
        {
            throw new NotImplementedException();
        }

        Task<Producto> IServicioCategoria.SaveCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }
    }
}
