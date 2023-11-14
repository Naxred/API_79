using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Repository
    {
        public static bool ValidaTexto(string Texto)
        {
            Boolean Validacion = false;

            // Eliminar espacios al principio y al final
            Texto = Texto.Trim();

            foreach (char Letra in Texto.Replace(" ", ""))
            {
                if (!char.IsLetter(Letra))
                {
                    Validacion = true;
                    break;
                }
            }

            // Si el texto está vacío después de quitar los espacios, no es válido
            if (string.IsNullOrEmpty(Texto))
            {
                Validacion = true;
            }

            return Validacion;
        }


    }
}
