using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using ProyectoInventario.Models.Entidades;
using ProyectoInventario.Services;
using System.Security.Claims;

namespace ProyectoInventario.Controllers
{
    public class ProductoController : Controller
    {

        private readonly IServicioImagenP _servicioImagenP;
        private readonly LibreriaContext _context;
        private readonly IServicioListaP _servicioListaP;

        public ProductoController(IServicioImagenP servicioImagenP, LibreriaContext context, IServicioListaP servicioListaP)
        {
            _servicioImagenP = servicioImagenP;
            _context = context;
            _servicioListaP = servicioListaP;
        }

        public async Task<IActionResult> ListadoProductos()
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

            var productosList = await _context.Productos
                 .Include(d => d.Categoria)
                  .Include(d => d.Bodega)
                  .Include(d => d.Proveedor)
                  .Include(d => d.Marca)


                  .ToListAsync();

            return View(productosList);
        }

        public async Task<IActionResult> Crear()
        {
            Producto producto = new Producto
            {
                Categorias = await _servicioListaP.GetListaCategorias(),
                Bodegas = await _servicioListaP.GetListaBodegas(),
                Proveedores = await _servicioListaP.GetListaProveedores(),
                Marcas = await _servicioListaP.GetListaMarcas()
            };

            // Asigna las listas al ViewData para que estén disponibles en la vista
            ViewData["Categoria"] = producto.Categorias;
            ViewData["Bodega"] = producto.Bodegas;
            ViewData["Proveedor"] = producto.Proveedores;
            ViewData["Marca"] = producto.Marcas;


            return View(producto);
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Producto producto)
        {
            // Cargar las listas antes de la validación del modelo
            producto.Categorias = await _servicioListaP.GetListaCategorias();
            producto.Bodegas = await _servicioListaP.GetListaBodegas();
            producto.Proveedores = await _servicioListaP.GetListaProveedores();
            producto.Marcas= await _servicioListaP.GetListaMarcas();

            if (producto.ProductoID == null)
            {
                // Debug: Recopilar mensajes de error detallados en una lista
                var errorMessages = new List<string>();
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        errorMessages.Add($"{modelStateKey}: {error.ErrorMessage}");
                    }
                }

                // Agregar mensajes de error a ModelState después de salir del bucle
                foreach (var errorMessage in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }

            else
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Producto creado exitosamente";

                return RedirectToAction("ListadoProductos");
            }

            return View(producto);
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            // Cargar las listas necesarias
            producto.Categorias = await _servicioListaP.GetListaCategorias();
            producto.Bodegas = await _servicioListaP.GetListaBodegas();
            producto.Proveedores = await _servicioListaP.GetListaProveedores();
            producto.Marcas = await _servicioListaP.GetListaMarcas();
            return View(producto);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Producto producto)
        {

            try
            {
                var autorexistente = await _context.Productos.FindAsync(producto.ProductoID);

                if (autorexistente == null)
                {
                    return NotFound();
                }

                // Resto del código...

                autorexistente.NombreProducto = producto.NombreProducto;
                autorexistente.Precio = producto.Precio;
                autorexistente.Stock = producto.Stock;
                autorexistente.Activo= producto.Activo;
                autorexistente.Categorias = producto.Categorias;
                autorexistente.Bodegas = producto.Bodegas;
                autorexistente.Proveedores = producto.Proveedores;
                autorexistente.Marcas = producto.Marcas;


                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Producto Actualizado exitosamente";

                return RedirectToAction("ListadoProductos");
            }
            catch (Exception ex)
            {
                TempData["alertMessage"] = ex;
            }



            // Cargar las listas antes de la validación del modelo
            producto.Categorias = await _servicioListaP.GetListaCategorias();
            producto.Bodegas = await _servicioListaP.GetListaBodegas();
            producto.Proveedores = await _servicioListaP.GetListaProveedores();
            producto.Marcas = await _servicioListaP.GetListaMarcas();

            return View(producto);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ProductoID == id);

            if (producto == null)
            {
                return NotFound();
            }

            try
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "LIbro eliminado exitosamente!!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");
            }

            return RedirectToAction(nameof(ListadoProductos));
        }

       
        

        }
    }
