using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using ProyectoInventario.Models.Entidades;
using ProyectoInventario.Services;
using System.Security.Claims;

namespace ProyectoInventario.Controllers
{
    public class BodegaController : Controller
    {
        private readonly LibreriaContext _context;

        public BodegaController(LibreriaContext contex)
        {
            _context = contex;
        }

        public async Task<IActionResult> ListadoBodegas()
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

            return View(await _context.Bodegas.ToListAsync());

        }

        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Crear(Bodega bodega)
        {

            if (ModelState.IsValid)
            {

                _context.Add(bodega);
                await _context.SaveChangesAsync();
                TempData["Alert Message"] = "Bodega Creado exitosamente";
                return RedirectToAction("ListadoBodegas");

            }

            else
            {
                ModelState.AddModelError(String.Empty, "Ha Ocurrido Un Error");
            }

            return View();

        }
        [HttpGet]

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.Bodegas == null)
            {
                return View();
            }

            var bodega= await _context.Bodegas.FindAsync(id);
            if (bodega== null)
            {
                return NotFound();
            }

            return View(bodega);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Bodega bodega)
        {
            if (id != bodega.BodegaID)
            {
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bodega);
                    await _context.SaveChangesAsync();

                    TempData["AlertMessage"] = "Bodegas actualizado: " + "exitosamente!!";

                    return RedirectToAction("ListadoBodegas");
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
            if (id == null || _context.Bodegas == null)
            {
                return View();
            }

            var bodega = await _context.Bodegas
                .FirstOrDefaultAsync(m => m.BodegaID == id);

            if (bodega== null)
            {
                return NotFound();
            }

            try
            {
                _context.Bodegas.Remove(bodega);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Bodega eliminado exitosamente!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");

            }

            return RedirectToAction(nameof(ListadoBodegas));

        }



    }
}
