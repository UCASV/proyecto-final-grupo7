using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.ContextUCA
{
    public partial class Appointment
    {
        public Appointment()
        {
            Vaccinations = new HashSet<Vaccination>();
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int IdPlaceVaccination { get; set; }
        public int IdCitizen { get; set; }

        public virtual Citizen IdCitizenNavigation { get; set; }
        public virtual PlaceVaccination IdPlaceVaccinationNavigation { get; set; }
        public virtual ICollection<Vaccination> Vaccinations { get; set; }
    }
}
