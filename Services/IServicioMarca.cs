using ProyectoInventario.Models.Entidades;

namespace ProyectoInventario.Services
{
    public interface IServicioMarca
    {
        Task<Marca> GetMarca(string NombreMarcas, string PaisOrigen );
        Task<Marca> SaveMarca(Marca marcas);

        Task<Marca> GetMarca(string NombreMarcas);


    }
}
