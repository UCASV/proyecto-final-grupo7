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
    }
}
