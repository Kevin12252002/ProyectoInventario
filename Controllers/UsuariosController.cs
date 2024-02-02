using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;

namespace ProyectoInventario.Controllers
{
    [Authorize(Policy = "RequireLoggedIn")]

    [Authorize(Roles = "Admin")]
    public class UsuariosController : Controller
    {
        private readonly LibreriaContext _context;

        public UsuariosController(LibreriaContext contex)
        {
            _context = contex;
        }

        public async Task<IActionResult> ListadoUsuario()
        {
            //  _context.Usuarios
            return View(await _context.Usuarios.ToListAsync());
        }
    }
}