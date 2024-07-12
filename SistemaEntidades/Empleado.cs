using System;
using System.Collections.Generic;

namespace SistemaEntidades
{
    public partial class Empleado
    {
        public Empleado()
        {
            Renta = new HashSet<Renta>();
        }

        public int IdEmpleado { get; set; }
        public string Dui { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public decimal SueldoBase { get; set; }
        public string Contrasena { get; set; } = null!;
        public int IdPuesto { get; set; }
        public string UrlImagen { get; set; } = null!;
        public bool? EsActivo { get; set; }

        public virtual Rol IdPuestoNavigation { get; set; } = null!;
        public virtual ICollection<Renta> Renta { get; set; }
    }
}
