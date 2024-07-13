using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaEntidades;

namespace Sistema.BLL.Interfaces
{
    public interface IUsuario
    {
        Task<List<Empleado>> Lista();
        Task<Empleado> Crear(Empleado entidad, Stream Foto = null, string NombreFoto = "", string UrlPlantillaCorreo = "");
        Task<Empleado> Editar(Empleado entidad, Stream Foto = null);
        Task<bool> Eliminar(int IdUsuario);
        Task<Empleado> ObtenerPorCredenciales(string correo, string clave);
        Task<Empleado> ObtenerPorId(int IdUsuario);
        Task<bool> GuardarPerfil(Empleado entidad);
        Task<bool> CambiarClave(string claveActual, string claveNueva, int IdUsuario);
        Task<bool> RestablecerClave(string correo, string urlPlatillaCorreo);
    }
}
