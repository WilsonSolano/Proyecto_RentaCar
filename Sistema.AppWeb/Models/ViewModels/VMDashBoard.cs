namespace Sistema.AppWeb.Models.ViewModels
{
    public class VMDashBoard
    {
        public int TotalRentas { get; set; }
        public int TotalIngresos { get; set;}
        public int TotalVehiculos { get;set;}
        public List<VMRentasSemana> VentasUltimaSemana { get; set; }
        public List<VMVehiculosSemana> VehiculosTopSemana { get; set; }
    }
}
