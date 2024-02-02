using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Models;
using ProyectoInventario.Models.Entidades;
using ProyectoInventario.Services;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ProyectoInventario.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServicioUsuario _servicioUsuario;
        private readonly IServicioImagen _servicioImagen;
        private readonly LibreriaContext _context;

        public LoginController(IServicioUsuario servicioUsuario,
            IServicioImagen servicioImagen, LibreriaContext context)
        {
            _servicioUsuario = servicioUsuario;
            _servicioImagen = servicioImagen;
            _context = context;
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Usuario usuario, IFormFile Imagen)
        {
            Stream image = Imagen.OpenReadStream();
            string urlImagen = await _servicioImagen.SubirImagen(image, Imagen.FileName);

            usuario.Clave = Utilizarios.EncriptarClave(usuario.Clave);
            usuario.URLFotoPerfil = urlImagen;

            Usuario usuarioCreado = await _servicioUsuario.SaveUsuario(usuario);

            if (usuarioCreado.IdUsuario > 0)
            {
                return RedirectToAction("IniciarSesion", "Login");
            }

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            Usuario usuarioEncontrado = await _servicioUsuario.GetUsuario(correo, Utilizarios.EncriptarClave(clave));

            if (usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }

            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuarioEncontrado.NombreUsuario),
        new Claim("FotoPerfil", usuarioEncontrado.URLFotoPerfil),
    };

            foreach (var rol in usuarioEncontrado.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            // Validación por cookies
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                // El usuario está autenticado por cookies, puedes realizar acciones adicionales si es necesario.
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult CerrarSesion()
        {
            // Lógica para cerrar la sesión
            // Aquí es donde generalmente se limpian los datos de la sesión

            // Redirige a la página de inicio de sesión
            return RedirectToAction("IniciarSesion");
        }
        public IActionResult VerificarRoles()
        {
            // Obtener la lista de roles del usuario
            var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            // Convertir la lista de roles en una cadena para mostrarla
            var rolesString = string.Join(", ", roles);

            // Mostrar la información de roles
            return Content($"Roles del usuario: {rolesString}");
        }
        public IActionResult MostrarClaims()
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            foreach (var claim in claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
            return Content("Revisa la consola del navegador o la salida de la aplicación para ver los claims.");
        }

    }
}
