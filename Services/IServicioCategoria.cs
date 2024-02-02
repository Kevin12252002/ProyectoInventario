using ProyectoInventario.Models.Entidades;

namespace ProyectoInventario.Services
{
    public interface IServicioCategoria
    {

        Task<Producto> GetCategoria(string NombreCategorias, string Descripcion, string TipoProducto);
        Task<Producto> SaveCategoria(Categoria categoria);

        Task<Producto> GetCategoria(string NombreCategorias);

        
            
       
    }
}
