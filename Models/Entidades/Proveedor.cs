using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInventario.Models.Entidades
{
    public class Proveedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProveedorID { get; set; }
        [Required(ErrorMessage = "El campo Nombre Completo es obligatorio.")]
        public string ?NombreProveedor { get; set; }
        public string ?Contacto { get; set; }
        public string ?Telefono { get; set; }
        public string ?Correo { get; set; }
    }
}
