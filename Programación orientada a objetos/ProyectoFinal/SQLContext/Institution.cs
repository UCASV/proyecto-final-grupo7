﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class Institution
    {
        public Institution()
        {
            Citizens = new HashSet<Citizen>();
        }

        public int Id { get; set; }
        public string Institution1 { get; set; }

        public virtual ICollection<Citizen> Citizens { get; set; }
    }
}
