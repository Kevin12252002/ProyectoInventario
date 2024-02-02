using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using ProyectoInventario.Models.Entidades;

namespace ProyectoInventario.Services
{
    public class ServicioBodega : IServicioBodega
    {

        private readonly LibreriaContext _context;

        public ServicioBodega(LibreriaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Bodega> GetBodega(string NombreBodega, string Ubicacion)
        {
            Bodega bodega = await _context.Bodegas.Where(u => u.NombreBodega == NombreBodega && u.Ubicacion == Ubicacion).FirstOrDefaultAsync();

            return bodega;
        }

        public async Task<Bodega> GetBodega(string NombreBodega)
        {
            return await _context.Bodegas.FirstOrDefaultAsync(u => u.NombreBodega == NombreBodega);
        }

        
       

        public async Task<Bodega> SaveBodega(Bodega bodega)
        {
            _context.Bodegas.Add(bodega);
            await _context.SaveChangesAsync();
            return bodega;
        }

        Task<Producto> IServicioBodega.GetBodega(string NombreBodega, string Ubicacion)
        {
            throw new NotImplementedException();
        }

        Task<Producto> IServicioBodega.GetBodega(string NombreBodega)
        {
            throw new NotImplementedException();
        }

        Task<Producto> IServicioBodega.SaveBodega(Bodega bodega)
        {
            throw new NotImplementedException();
        }
    }
}
