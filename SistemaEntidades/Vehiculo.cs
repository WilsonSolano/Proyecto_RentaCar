using System;
using System.Collections.Generic;

namespace SistemaEntidades
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Renta = new HashSet<Renta>();
        }

        public int IdVehiculo { get; set; }
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int Anio { get; set; }
        public string? Descripcion { get; set; }
        public decimal PrecioRenta { get; set; }
        public string Placas { get; set; } = null!;
        public bool? Disponible { get; set; }
        public string? UrlImagen { get; set; }
        public string? NombreImagen { get; set; }

        public virtual ICollection<Renta> Renta { get; set; }
    }
}
