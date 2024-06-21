using System;
using System.Collections.Generic;

namespace SistemaEntidades
{
    public partial class Factura
    {
        public int IdFactura { get; set; }
        public int IdRenta { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal Monto { get; set; }

        public virtual Renta IdRentaNavigation { get; set; } = null!;
    }
}
