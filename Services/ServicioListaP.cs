using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;

namespace ProyectoInventario.Services
{
    public class ServicioListaP: IServicioListaP
    {
        private readonly LibreriaContext _context;

        public ServicioListaP(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetListaCategorias()
        {
            List<SelectListItem> list = await _context.Categorias.Select(x => new SelectListItem
            {
                Text = x.NombreCategorias,

                Value = $"{x.CategoriaID}"
            })
            .OrderBy(x => x.Text)
            .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Categoria....]",
                Value = "0"

            });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetListaBodegas()
        {
            List<SelectListItem> list = await _context.Bodegas.Select(x => new SelectListItem
            {
                Text = x.NombreBodega,

                Value = $"{x.BodegaID}"
            })
            .OrderBy(x => x.Text)
            .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Bodega....]",
                Value = "0"

            });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetListaProveedores()
        {
            List<SelectListItem> list = await _context.Proveedors.Select(x => new SelectListItem
            {
                Text = x.NombreProveedor,

                Value = $"{x.ProveedorID}"
            })
            .OrderBy(x => x.Text)
            .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Proveedor....]",
                Value = "0"

            });
            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetListaMarcas()
        {
            List<SelectListItem> list = await _context.Marcas.Select(x => new SelectListItem
            {
                Text = x.NombreMarcas,

                Value = $"{x.MarcaID}"
            })
            .OrderBy(x => x.Text)
            .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Marca....]",
                Value = "0"

            });
            return list;
        }
    }
}
