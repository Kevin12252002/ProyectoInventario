using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Models.Entidades;
using ProyectoInventario.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ProyectoInventario.Controllers
{
    public class MarcaController : Controller
    {
        private readonly LibreriaContext _context;

        public MarcaController(LibreriaContext contex)
        {
            _context = contex;
        }


        public async Task<IActionResult> ListadoMarcas()

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

            return View(await _context.Marcas.ToListAsync());


        }

        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Crear(Marca marcas)
        {

            if (ModelState.IsValid)
            {

                _context.Add(marcas);
                await _context.SaveChangesAsync();
                TempData["Alert Message"] = " Marca Creado exitosamente";
                return RedirectToAction("ListadoMarcas");

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
            if (id == null || _context.Marcas == null)
            {
                return View();
            }

            var marcas = await _context.Marcas.FindAsync(id);
            if (marcas == null)
            {
                return NotFound();
            }

            return View(marcas);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Marca marcas)
        {
            if (id != marcas.MarcaID)
            {
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marcas);
                    await _context.SaveChangesAsync();

                    TempData["AlertMessage"] = "Marca actualizada: " + "exitosamente!!";

                    return RedirectToAction("ListadoMarcas");
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
            if (id == null || _context.Marcas == null)
            {
                return View();
            }

            var marcas = await _context.Marcas
                .FirstOrDefaultAsync(m => m.MarcaID == id);

            if (marcas == null)
            {
                return NotFound();
            }

            try
            {
                _context.Marcas.Remove(marcas);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Marca eliminada exitosamente!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");

            }

            return RedirectToAction(nameof(ListadoMarcas));


        }




    }
}