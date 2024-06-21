using System;
using System.Collections.Generic;

namespace SistemaEntidades
{
    public partial class Renta
    {
        public Renta()
        {
            Facturas = new HashSet<Factura>();
            Multa = new HashSet<Multa>();
        }

        public int IdRenta { get; set; }
        public int IdCliente { get; set; }
        public int IdVehiculo { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Total { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Multa> Multa { get; set; }
    }
}
