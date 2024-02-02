using ProyectoInventario.Models.Entidades;

namespace ProyectoInventario.Services
{
    public interface IServicioBodega
    {

        Task<Producto> GetBodega(string NombreBodega, string Ubicacion);
        Task<Producto> SaveBodega(Bodega bodega);

        Task<Producto> GetBodega(string NombreBodega);
       
    }
}
