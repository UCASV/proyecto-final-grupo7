using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Controller
{
    class SideEffectsException : Exception
    {
        public SideEffectsException() : base()
        {

        }
        public SideEffectsException(string message) : base(message)
        {

        }
    }
}
