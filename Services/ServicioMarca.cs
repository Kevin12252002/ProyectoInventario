using ProyectoInventario.Models.Entidades;
using ProyectoInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoInventario.Services
{
    public class ServicioMarca : IServicioMarca
    {
        private readonly LibreriaContext _context;

        public ServicioMarca(LibreriaContext context)
        {
            _context = context;

        }

        public async Task<Marca> GetMarca(string NombreMarcas, string PaisOrigen)
        {
            Marca marcas = await _context.Marcas.Where(u => u.NombreMarcas == NombreMarcas && u.PaisOrigen == PaisOrigen).FirstOrDefaultAsync();

            return marcas;
        }

        public async Task<Marca> GetMarca(string NombreMarcas)
        {
            return await _context.Marcas.FirstOrDefaultAsync(u => u.NombreMarcas == NombreMarcas);
        }



        public async Task<Marca> SaveCategoria(Marca marcas)
        {
            _context.Marcas.Add(marcas);
            await _context.SaveChangesAsync();
            return marcas;
        }

        Task<Marca> IServicioMarca.GetMarca(string NombreMarcas, string PaisOrigen)
        {
            throw new NotImplementedException();
        }

        Task<Marca> IServicioMarca.GetMarca(string NombreMarcas)
        {
            throw new NotImplementedException();
        }

        Task<Marca> IServicioMarca.SaveMarca(Marca marcas)
        {
            throw new NotImplementedException();
        }
    }
}
