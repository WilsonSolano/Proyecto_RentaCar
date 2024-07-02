using Sistema.AppWeb.Models.ViewModels;
using SistemaEntidades;
using System.Globalization;
using AutoMapper;

namespace Sistema.AppWeb.Utilidades.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, VMRol>().ReverseMap();
            #endregion Rol

            #region Empleado
            CreateMap<Empleado, VMEmpleado>().ReverseMap();
            #endregion

            #region Vehiculo
            CreateMap<Vehiculo, VMVehiculo>().ReverseMap();
            #endregion

            #region Empleado
            CreateMap<Empleado, VMEmpleado>().ReverseMap();
            #endregion

            #region Renta
            CreateMap<VMRenta, Renta>().
                ForMember(destino => destino.FechaFin,
                opt => opt.MapFrom(origen => origen.FechaFin.ToString("dd/MM/yyyy")));

            CreateMap<Renta, VMRenta>().ForMember(destino => destino.IdEmpleado,
                opt => opt.MapFrom(origen => origen.IdEmpleadoNavigation.Nombre)).
                ForMember(destino => destino.Total,
                opt => opt.MapFrom(opt => Convert.ToString(opt.Total, new CultureInfo("es-PE")))).
                ForMember(destino => destino.FechaFin,
                opt => opt.MapFrom(origen => origen.FechaFin.ToString("dd/MM/yyyy")));
            #endregion

            #region Factura
            CreateMap<Factura, VMFactura>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, VMMenu>().ForMember(destino => 
            destino.SubMenus,
            opt => opt.MapFrom(origen => origen.InverseIdMenuPadreNavigation));
            #endregion
        }
    }
}
