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
        
        public string? NombreProducto { get; set; }

        public decimal Precio { get; set; }
        public int Stock { get; set; }
       
        public bool Activo { get; set; } = true;




       

        

        public Categoria ?Categoria { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría.")]
        public int CategoriaID { get; set; }

        public Bodega ?Bodega { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una bodega.")]
        public int BodegaID { get; set; }

        public Proveedor ?Proveedor { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proveedor.")]
        public int ProveedorID { get; set; }


        public Marca ?Marca { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una marca.")]
        public int MarcaID { get; set; }



        [NotMapped]
        public IEnumerable<SelectListItem> ?Categorias { get; set; }

        [NotMapped]

        public IEnumerable<SelectListItem> ?Bodegas { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> ?Proveedores { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> ?Marcas{ get; set; }



    }
}
