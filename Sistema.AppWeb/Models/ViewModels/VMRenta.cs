using SistemaEntidades;

namespace Sistema.AppWeb.Models.ViewModels
{
    public class VMRenta
    {
        public int IdRenta { get; set; }
        public int IdCliente { get; set; }
        public int IdVehiculo { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Total { get; set; }
    }
}
