using ProyectoInventario.Models.Entidades;
using ProyectoInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoInventario.Services
{
    public class ServicioRoles : IServicioRoles
    {

        private readonly LibreriaContext _context;  

        public ServicioRoles(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<Roles> SaveRoles(Roles roles)
        {

            _context.Roles.Add(roles);
            await _context.SaveChangesAsync();
            return roles;

        }

        public async Task<Roles> Get(string NombreRol)
        {
            return await _context.Roles.FirstOrDefaultAsync(c => c.NombreRol == NombreRol);
        }

    }
}