using ProyectoFinal.ContextUCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Controller
{
    class Validation
    {
        public static void ValidateDates(DateTime date1, DateTime date2)
        {
            if (date1.Date != date2.Date || date1.Date < date2.Date || date1.Date > date2.Date)
            {
                throw new DateException("Las fechas deben de coincidir");
            }
        }
        public static void ValidateSelectedItem(int minutes, EffectSecondary secondary)
        {
            if ((minutes > 0 && secondary.Id == 1) || (minutes == 0 && secondary.Id != 1))
            {
                throw new SideEffectsException("Debe de seleccionar el efecto secundario presentado en " +
                    "el lapso de tiempo indicado o debe de seleccionar en que minuto surgió el efecto secundario");
            }
        }
    }
}
