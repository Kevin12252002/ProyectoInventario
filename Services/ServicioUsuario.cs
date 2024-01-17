using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using ProyectoInventario.Models.Entidades;
using ProyectoInventario.Services;
using System;
using System.Threading.Tasks;

namespace ProyectoInventario.Services
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly LibreriaContext _context;

        public ServicioUsuario(LibreriaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario = await _context.Usuarios
                .Where(u => u.Correo == correo && u.Clave == clave).FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<Usuario> GetUsuario(string nombreUsuario)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        }

        public async Task<Usuario> SaveUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "El objeto de usuario no puede ser nulo.");
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
