using Firebase.Auth;
using Firebase.Storage;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace ProyectoInventario.Services
{

    public class ServicioImagen : IServicioImagen
    {
        public async Task<string> SubirImagen(Stream archivo, string nombre)
        {
            string email = "kevinq_25@hotmail.com";
            string clave = "1751764406";
            string ruta = "proyectoinventario-f1db9.appspot.com";
            string api_key = "AIzaSyCj8m_iCG36l9cWHzSaEdG33wom6cBV5UM";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
              ruta,
              new FirebaseStorageOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                  ThrowOnCancel = true
              })

               .Child("Fotos_Perfil")
               .Child(nombre)
               .PutAsync(archivo, cancellation.Token);

            var downloadURL = await task;
            return downloadURL;
        }
    }
}