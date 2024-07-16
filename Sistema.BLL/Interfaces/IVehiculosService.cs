using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaEntidades;

namespace Sistema.BLL.Interfaces
{
    public interface IVehiculosService
    {
        Task<List<Vehiculo>> Lista();
        Task<Vehiculo> Crear(Vehiculo entidad, Stream imagen = null, string nombreImagen = "");
        Task<Vehiculo> Editar(Vehiculo entidad, Stream imagen = null);
        Task<bool> Eliminar(int idVehiculo);
    }
}
