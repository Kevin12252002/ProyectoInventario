using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProyectoInventario.Models.Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoInventario.Models.Entidades
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoID { get; set; }
        [Required(ErrorMessage = "El campo Nombre Completo es obligatorio.")]


        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? URLFotoProducto { get; set; }
        public bool Activo { get; set; } = true;
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public Bodega bodega { get; set; }

        public Categoria categoria { get; set; }

        public Proveedor proveedor { get; set; }

    }
}
