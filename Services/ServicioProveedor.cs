using ProyectoInventario.Models.Entidades;
using ProyectoInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoInventario.Services
{
    public class ServicioProveedor : IServicioProveedor
    {
        private readonly LibreriaContext _context;

        public ServicioProveedor(LibreriaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Proveedor> GetProveedor(string NombreProveedor, string Contacto, string Telefono, string Correo)
        {
            Proveedor proveedor = await _context.Proveedors.Where(u => u.NombreProveedor == NombreProveedor && u.Contacto == Contacto && u.Telefono == Telefono && u.Correo == Correo).FirstOrDefaultAsync();

            return proveedor;
        }

        public async Task<Proveedor> GetProveedor(string NombreProveedor)
        {
            return await _context.Proveedors.FirstOrDefaultAsync(u => u.NombreProveedor == NombreProveedor);
        }

       
        public Task<Producto> SaveCProveedor(Proveedor proveedor)
        {
            throw new NotImplementedException();
        }

        public async Task<Proveedor> SaveProveedor(Proveedor proveedor)
        {
            _context.Proveedors.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        Task<Producto> IServicioProveedor.GetProveedor(string NombreProveedor, string Contacto, string Telefono, string Correo)
        {
            throw new NotImplementedException();
        }

        Task<Producto> IServicioProveedor.GetProveedor(string NombreProveedor)
        {
            throw new NotImplementedException();
        }
    }
}

