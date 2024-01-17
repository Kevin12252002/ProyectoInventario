using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoInventario.Services
{
    public interface IServicioListaP
    {
        Task<IEnumerable<SelectListItem>>
            GetListaBodegas();
        Task<IEnumerable<SelectListItem>>
            GetListaCategorias();
        Task<IEnumerable<SelectListItem>>
            GetListaProveedores();
        Task<IEnumerable<SelectListItem>>
         GetListaProductos();


    }
}
