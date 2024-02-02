using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInventario.Models.Entidades
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaID { get; set; }
        [Required(ErrorMessage = "El campo Nombre Completo es obligatorio.")]
        public string? NombreCategorias { get; set; }  
        public string?Descripcion { get; set; }
        public string? TipoProducto { get; set; }

    }
}
