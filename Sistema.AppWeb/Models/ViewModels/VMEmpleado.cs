using SistemaEntidades;

namespace Sistema.AppWeb.Models.ViewModels
{
    public class VMEmpleado
    {
        public int IdEmpleado { get; set; }
        public string Dui { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public decimal SueldoBase { get; set; }
        public int IdPuesto { get; set; }
        public string Descripcion { get; set; } = null!;
        public int es_activo { get; set; }
        public string UrlImagen { get; set; } = null!;
    }
}
