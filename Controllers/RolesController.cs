using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Models.Entidades;
using ProyectoInventario.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoInventario.Controllers
{
    [Authorize(Policy = "RequireLoggedIn")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly LibreriaContext _context;


        public RolesController(LibreriaContext contex)
        {
            _context = contex;
        }

        public async Task<IActionResult> ListadoRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            var totalRoles = await ObtenerTotalRoles();
            ViewBag.TotalRoles = totalRoles;
            return View(roles);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Roles roles)
        {

            if (ModelState.IsValid)
            {
                _context.Add(roles);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Roles creado exitosamente";
                return RedirectToAction("ListadoRoles");

            }
            else
            {
                ModelState.AddModelError(String.Empty, "Ha ocurrido Un error");
            }



            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            return View(roles);
        }


        [HttpPost]
        public async Task<IActionResult> Editar(int id, Roles roles)
        {
            if (id != roles.RolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roles);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Rol actualizado " +
                        "exitosamente!!!";
                    return RedirectToAction("ListadoRoles");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(ex.Message, "Ocurrio un error " +
                        "al actualizar");
                }
            }
            return View(roles);
        }


        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.RolID == id);

            if (roles == null)
            {
                return NotFound();
            }

            try
            {
                _context.Roles.Remove(roles);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Rol eliminado exitosamente!!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");
            }

            return RedirectToAction(nameof(ListadoRoles));
        }
        public IActionResult Index()
        {
            return View();
        }








       /// <summary>
       /// ///////
       /// </summary>
       /// <returns></returns>
        public async Task<int> ObtenerTotalRoles()
        {
            return await _context.Roles.CountAsync();
        }


    }
}