using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInventario.Models.Entidades
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string? URLFotoPerfil { get; set; }

        public string Correo { get; set; } = null!;

        public string Clave { get; set; } = null!;
    }
}
