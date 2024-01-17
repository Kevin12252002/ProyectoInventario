using ProyectoInventario.Models.Entidades;

namespace ProyectoInventario.Services
{
    public interface IServicioProducto 
    {
        Task<Producto> GetProducto(string Nombre, decimal Precio, int Stock, string URLFotoProducto , bool Activo,string Marca,string Modelo );
        Task<Producto> SaveProducto(Producto producto);

        Task<Producto> GetProducto(string Nombre);
    }
}
