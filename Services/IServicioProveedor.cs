using ProyectoInventario.Models.Entidades;

namespace ProyectoInventario.Services
{
    public interface IServicioProveedor
    {
        Task<Producto> GetProveedor(string NombreProveedor, string Contacto , string Telefono , string Correo);
        Task<Producto> SaveCProveedor(Proveedor proveedor);

        Task<Producto> GetProveedor(string NombreProveedor);


     
    }
}
