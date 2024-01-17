using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using ProyectoInventario.Models.Entidades;
using System.Security.Claims;

namespace ProyectoInventario.Controllers
{
    public class ProveedorController : Controller
    {

        private readonly LibreriaContext _context;

        public ProveedorController(LibreriaContext contex)
        {
            _context = contex;
        }

        public async Task<IActionResult> ListadoProveedores()

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

            return View(await _context.Proveedors.ToListAsync());
        }

        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Crear(Proveedor proveedor)
        {

            if (ModelState.IsValid)
            {

                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                TempData["Alert Message"] = "Proveedor Creado exitosamente";
                return RedirectToAction("ListadoProveedores");

            }
            else
            {
                ModelState.AddModelError(String.Empty, "Ha Ocurrido Un Error");
            }

            return View();

        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.Proveedors == null)
            {
                return View();
            }
            var proveedor = await _context.Proveedors.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }


        [HttpPost]
        public async Task<IActionResult> Editar(int id, Proveedor proveedor)
        {
            if (id != proveedor.ProveedorID)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();

                    TempData["AlertMessage"] = "Proveedor actualizado: " + "exitosamente!!";

                    return RedirectToAction("ListadoProveedores");
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.Message, "Ocurrio Un Error " + "Al Actualizar");

                }
            }

            return View();
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Proveedors == null)
            {
                return View();
            }
            var proveedor = await _context.Proveedors
                .FirstOrDefaultAsync(m => m.ProveedorID == id);

            if (proveedor == null)
            {
                return NotFound();
            }
            try
            {
                _context.Proveedors.Remove(proveedor);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Proveedor eliminado exitosamente!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");

            }

            return RedirectToAction(nameof(ListadoProveedores));

        }

    }
}
