using System;
using System.Collections.Generic;

namespace SistemaEntidades
{
    public partial class Multa
    {
        public int IdMulta { get; set; }
        public int IdRenta { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Categoria { get; set; } = null!;

        public virtual Renta IdRentaNavigation { get; set; } = null!;
    }
}
