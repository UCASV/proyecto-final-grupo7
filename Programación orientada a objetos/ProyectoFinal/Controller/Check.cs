using ProyectoFinal.ContextUCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Controller
{
    public static class Check
    {
        //Validando la Institucion que pertenece el ciudadano
        public static bool Check_institution(int n)
        {
            bool status;
            switch (n)
            {
                case 204: //Cuerpo de Salud

                case 303: // PNC

                case 404: // Gobierno

                case 505: //Fuerza Armada

                case 707: //Cuerpo de socorro

                case 808: //Personal de fronteras

                case 909: status = true; break;//Centro de Penales

                default: status = false; break;
            }

            return status;
        }

        //Metodo para validar en numero de DUI
        public static bool CheckDui(string dui)
        {
            const string digits = "0123456789";

            if (dui.Length != 10)
            {
                return false;
            }

            for (int i = 0; i < dui.Length; i++)
            {
                if ((i == 8 && dui[i] != '-') || (i != 8 && !digits.Contains(dui[i])))
                    return false;

            }
            return true;
        }

    }
}
