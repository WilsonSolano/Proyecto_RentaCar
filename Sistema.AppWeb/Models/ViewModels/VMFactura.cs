namespace Sistema.AppWeb.Models.ViewModels
{
    public class VMFactura
    {
        public int IdFactura { get; set; }
        public int IdRenta { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal Monto { get; set; }

        public ICollection<VMRenta> Rentas { get; set; }
    }
}
