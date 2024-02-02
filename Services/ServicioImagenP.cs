using Firebase.Auth;
using Firebase.Storage;

namespace ProyectoInventario.Services
{
    public class ServicioImagenP : IServicioImagenP
    {
        public async Task<string> SubirImagen(Stream archivo, string nombre)
        {
            try
            {
                string email = "kevinq_2002@outlook.com";
                string clave = "1234567";
                string ruta = "imagenesproducto-1.appspot.com";  // Por ejemplo, "proyectoinventario-f1db9.appspot.com"
                string api_key = "AIzaSyA9DUcJuoEYbfLjMFhRxILscefog9GmZYY";

                var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
                var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

                var cancellation = new System.Threading.CancellationTokenSource();

                var storage = new FirebaseStorage(
                    ruta,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    });

                var subida = await storage
                    .Child("Fotos_Producto")
                    .Child(nombre)
                    .PutAsync(archivo, cancellation.Token);

                return subida;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al subir la imagen: {ex.Message}");
                // Puedes decidir relanzar la excepción o manejarla de forma diferente según tus necesidades.
                throw;
            }
        }
    }
        }
    
