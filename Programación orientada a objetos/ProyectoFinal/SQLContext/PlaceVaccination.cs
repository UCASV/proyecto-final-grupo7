﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class PlaceVaccination
    {
        public PlaceVaccination()
        {
            Vaccinations = new HashSet<Vaccination>();
        }

        public int Id { get; set; }
        public string PlaceVaccination1 { get; set; }

        public virtual ICollection<Vaccination> Vaccinations { get; set; }
    }
}
