using System;
using System.Collections.Generic;

namespace SistemaEntidades
{
    public partial class Cliente
    {
        public Cliente()
        {
            Renta = new HashSet<Renta>();
        }

        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Dui { get; set; } = null!;
        public string TipoLicencia { get; set; } = null!;

        public virtual ICollection<Renta> Renta { get; set; }
    }
}
