using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using System.Security.Claims;

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
            ClaimsPrincipal claimsUser = HttpContext.User;
            string nombreUsuario = "";
            string fotoPerfil = "";

            if (claimsUser.Identity.IsAuthenticated)
            {
                nombreUsuario = claimsUser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();

                fotoPerfil = claimsUser.Claims.Where(c => c.Type == "FotoPerfil")
                    .Select(c => c.Value).SingleOrDefault();
            }

            ViewData["nombreUsuario"] = nombreUsuario;
            ViewData["fotoPerfil"] = fotoPerfil;

          

            //  _context.Usuarios
            return View(await _context.Usuarios.ToListAsync());
        }
    }
}