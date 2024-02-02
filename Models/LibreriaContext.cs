using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models.Entidades;
using System.Data;


namespace ProyectoInventario.Models
{


    public class LibreriaContext : DbContext
    { 

        public LibreriaContext()
        {

        }

        //opciones de base de datos 
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {

        }

      

        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Proveedor> Proveedors { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Roles> Roles { get; set; }



        //modelo de creaccion que hace que la base de datos se conecte con el aplicativo de visual estudio 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasIndex(c => c.NombreUsuario).IsUnique();
        }
    }
       

         

            // Agrega más configuraciones según tus necesidades
        }
    
