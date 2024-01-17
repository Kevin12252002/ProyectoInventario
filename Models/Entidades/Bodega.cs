using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoInventario.Models.Entidades
{
    public class Bodega
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BodegaID { get; set; }
        [Required(ErrorMessage = "El campo Nombre Completo es obligatorio.")]
        public string NombreBodega { get; set; }
        public string Ubicacion { get; set; }


    }
}
