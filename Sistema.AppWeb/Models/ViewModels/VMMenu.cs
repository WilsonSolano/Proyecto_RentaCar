namespace Sistema.AppWeb.Models.ViewModels
{
    public class VMMenu
    {
        public int IdMenu { get; set; }
        public string? Descripcion { get; set; }
        public int? IdMenuPadre { get; set; }
        public string? Icono { get; set; }
        public string? Controlador { get; set; }
        public string? PaginaAccion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public ICollection<VMMenu> SubMenus { get; set; }
    }
}
