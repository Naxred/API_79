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

            foreach (char Letra in Texto.Replace(" ", ""))
            {
                if (!char.IsLetter(Letra))
                {
                    Validacion = true;
                    break;
                }
            }

            return Validacion;
        }


    }
}
