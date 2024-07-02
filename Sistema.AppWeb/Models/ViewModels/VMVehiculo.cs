﻿namespace Sistema.AppWeb.Models.ViewModels
{
    public class VMVehiculo
    {
        public int IdVehiculo { get; set; }
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int Anio { get; set; }
        public string? Descripcion { get; set; }
        public decimal PrecioRenta { get; set; }
        public string Placas { get; set; } = null!;
        public int? Disponible { get; set; }
        public string? UrlImagen { get; set; }

    }
}