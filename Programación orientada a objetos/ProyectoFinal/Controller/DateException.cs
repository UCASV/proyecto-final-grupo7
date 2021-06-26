using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Controller
{
    class DateException : Exception
    {
        public DateException() : base()
        {

        }
        public DateException(string message) : base(message)
        {

        }
    }
}
