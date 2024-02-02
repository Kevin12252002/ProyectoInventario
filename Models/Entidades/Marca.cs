using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInventario.Models.Entidades
{
    public class Marca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarcaID { get; set; }


        [Required(ErrorMessage = "El campo Nombre Completo es obligatorio.")]
        public string? NombreMarcas { get; set; }

        public string? PaisOrigen { get; set; }
    }
}
