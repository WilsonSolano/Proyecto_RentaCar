﻿using System;
using System.Collections.Generic;

namespace SistemaEntidades
{
    public partial class Rol
    {
        public Rol()
        {
            Empleados = new HashSet<Empleado>();
            RolMenus = new HashSet<RolMenu>();
        }

        public int IdRol { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
        public virtual ICollection<RolMenu> RolMenus { get; set; }
    }
}
