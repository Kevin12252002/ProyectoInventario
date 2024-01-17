using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using ProyectoInventario.Models.Entidades;

using ProyectoInventario.Services;
using System.Security.Claims;

namespace ProyectoInventario.Controllers
{
    public class CategoriaController : Controller
    {

        private readonly LibreriaContext _context;

        public CategoriaController(LibreriaContext contex)
        {
            _context = contex;
        }


        public async Task<IActionResult> ListadoCategorias()

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

            return View(await _context.Categorias.ToListAsync());

           
        }

        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Crear(Categoria categoria)
        {

            if (ModelState.IsValid)
            {

                _context.Add(categoria);
                await _context.SaveChangesAsync();
                TempData["Alert Message"] = "Categoria Creado exitosamente";
                return RedirectToAction("ListadoCategorias");

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
            if (id == null || _context.Categorias == null)
            {
                return View();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();

                    TempData["AlertMessage"] = "Categoria actualizado: " + "exitosamente!!";

                    return RedirectToAction("ListadoCategorias");
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
            if (id == null || _context.Categorias == null)
            {
                return View();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.CategoriaID== id);

            if (categoria == null)
            {
                return NotFound();
            }

            try
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Categoria eliminado exitosamente!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");

            }

            return RedirectToAction(nameof(ListadoCategorias));

        }



    }
}