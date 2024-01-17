using System.Security.Cryptography;
using System.Text;

namespace ProyectoInventario.Services
{
    public class Utilizarios
    {
        public static string EncriptarClave(string clave)
        {
            using(SHA256 sHA256 = SHA256.Create())
            {
                byte[] bytes= Encoding.UTF8.GetBytes(clave);
                byte[] hash = sHA256.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));// formato hexadecimal

                }
                return sb.ToString();
            }
        }
    }
}
