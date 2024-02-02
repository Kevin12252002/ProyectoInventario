using ProyectoInventario.Models.Entidades;

namespace ProyectoInventario.Services
{
    public interface IServicioRoles
    {
        Task<Roles> SaveRoles(Roles roles);

        Task<Roles> Get(string NombreRol);
    }
}
