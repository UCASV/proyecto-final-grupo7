using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.ContextUCA
{
    public partial class PlaceVaccination
    {
        public PlaceVaccination()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string PlaceVaccination1 { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
