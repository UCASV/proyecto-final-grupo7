using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.ContextUCA
{
    public partial class Vaccination
    {
        public int Id { get; set; }
        public DateTime DateTimeApplication { get; set; }
        public DateTime DateTimeProcess { get; set; }
        public string TimeSecondaryEffect { get; set; }
        public int? IdEffectSecondary { get; set; }
        public int IdAppointment { get; set; }

        public virtual Appointment IdAppointmentNavigation { get; set; }
        public virtual EffectSecondary IdEffectSecondaryNavigation { get; set; }
    }
}
