using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Sistema.BLL.Interfaces;
using Sistema.DAL.Interfaces;
using SistemaEntidades;

namespace Sistema.BLL.Implementacion
{
    public class EmpleadoService : IUsuario
    {
        private readonly IGenericRepository<Empleado> _repository;
        private readonly IFireBaseService _fireBaseService;
        private readonly IUtilidadesService _utilidadesService;
        private readonly ICorreoService _correoService;

        public EmpleadoService(IGenericRepository<Empleado> repository, IFireBaseService fireBaseService, IUtilidadesService utilidadesService, ICorreoService correoService)
        {
            _repository = repository;
            _fireBaseService = fireBaseService;
            _utilidadesService = utilidadesService;
            _correoService = correoService;
        }

        public async Task<List<Empleado>> Lista()
        {
            IQueryable<Empleado> query = await _repository.Consultar();
            return query.Include(r => r.IdPuestoNavigation).ToList();
        }

        public async Task<Empleado> Crear(Empleado entidad, Stream Foto = null, string NombreFoto = "", string UrlPlantillaCorreo = "")
        {
            Empleado empleado_existe = await _repository.Obtener(e => e.Email == entidad.Email);
            if(empleado_existe != null)
                throw new TaskCanceledException("El correo ya esta bajo uso");
            try
            {
                string clave_generada = _utilidadesService.GenerarClave();
                entidad.Contrasena = _utilidadesService.ConvertirSha256(clave_generada);
                
                if(Foto != null)
                {
                    string url = await _fireBaseService.subirStorage(Foto, "carpeta_usuario", NombreFoto);
                    entidad.UrlImagen = url;
                }

                Empleado empleado_creado = await _repository.Crear(entidad);

                if (empleado_creado.IdEmpleado == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el usuario");
                }

                if (UrlPlantillaCorreo != "")
                {
                    UrlPlantillaCorreo = UrlPlantillaCorreo.Replace("[correo]", empleado_creado.Email).Replace("[clave]", clave_generada);

                    string htmlCorreo = "";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlPlantillaCorreo);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream dataStream = response.GetResponseStream())
                        {
                            StreamReader streamReader = null;

                            if (response.CharacterSet == null)
                                streamReader = new StreamReader(dataStream);
                            else
                                streamReader = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                            htmlCorreo = streamReader.ReadToEnd();
                            response.Close();
                            streamReader.Close();
                        }
                    }

                    if (htmlCorreo != "")
                        await _correoService.EnviarCorreo(empleado_creado.Email, "Cuenta Creada", htmlCorreo);
                }

                IQueryable<Empleado> query = await _repository.Consultar(e => e.IdEmpleado == empleado_creado.IdEmpleado);
                empleado_creado = query.Include(r => r.IdPuestoNavigation).First();

                return empleado_creado;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Empleado> Editar(Empleado entidad, Stream Foto = null, string NombreFoto = "")
        {
            Empleado empleado_existe = await _repository.Obtener(e => e.Email == entidad.Email && e.IdEmpleado != entidad.IdEmpleado);
            if (empleado_existe != null)
                throw new TaskCanceledException("El correo ya esta bajo uso");

            try
            {
                IQueryable<Empleado> queryUsuario = await _repository.Consultar(e => e.IdEmpleado == entidad.IdEmpleado);
                Empleado empleado_editar = queryUsuario.First();

                empleado_editar.Nombre = entidad.Nombre;
                empleado_editar.Email = entidad.Email;
                empleado_editar.Contrasena = entidad.Contrasena;
                empleado_editar.IdPuesto = entidad.IdPuesto;


                bool respuesta = await _repository.Editar(empleado_editar);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo modificar el Empleado");
                }

                Empleado empleado_editado = queryUsuario.Include(r => r.IdPuestoNavigation).First();

                return empleado_editado;
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> Eliminar(int IdUsuario)
        {
            try
            {
                Empleado empleado_encontrado = await _repository.Obtener(e => e.IdEmpleado == IdUsuario);

                if (empleado_encontrado == null)
                    throw new TaskCanceledException("El Usuario No Existe");

                bool respuesta = await _repository.Eliminar(empleado_encontrado);

                return true;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public async Task<Empleado> ObtenerPorCredenciales(string correo, string clave)
        {
            string clave_encriptada = _utilidadesService.ConvertirSha256(clave);

            Empleado empleado_encontrado = await _repository.Obtener(e => e.Email.Equals(correo) && e.Contrasena.Equals(clave_encriptada));

            return empleado_encontrado;
        }

        public async Task<Empleado> ObtenerPorId(int IdUsuario)
        {
            IQueryable<Empleado> query = await _repository.Consultar(e => e.IdEmpleado == IdUsuario);

            Empleado resultado = query.Include(r => r.IdPuestoNavigation).FirstOrDefault();

            return resultado;
        }

        public async Task<bool> GuardarPerfil(Empleado entidad)
        {
            try
            {
                Empleado empleado_encontrado = await _repository.Obtener(e => e.IdEmpleado == entidad.IdEmpleado);
                
                if(empleado_encontrado == null)
                    throw new TaskCanceledException("El Usuario no Existe");

                empleado_encontrado.Email = entidad.Email;
                
                bool respuesta = await _repository.Editar(empleado_encontrado);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> CambiarClave(string claveActual, string claveNueva, int IdUsuario)
        {
            try
            {
                Empleado empleado_encontrado = await _repository.Obtener(e => e.IdEmpleado == IdUsuario);

                if (empleado_encontrado == null)
                    throw new TaskCanceledException("El Usuario no Existe");

                if(empleado_encontrado.Contrasena != _utilidadesService.ConvertirSha256(claveNueva))
                    throw new TaskCanceledException("La contraseña ingresada como actual no es correcta");

                empleado_encontrado.Contrasena = _utilidadesService.ConvertirSha256(claveNueva);

                bool respuesta = await _repository.Editar(empleado_encontrado);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RestablecerClave(string correo, string UrlPlantillaCorreo = "")
        {
            try
            {
                Empleado empleado_encontrado = await _repository.Obtener(e => e.Email == correo);

                if (empleado_encontrado == null)
                    throw new TaskCanceledException("Ningun empleado se encuentra asociado al correo ingresado");

                string clave_generada = _utilidadesService.GenerarClave();

                empleado_encontrado.Contrasena = _utilidadesService.ConvertirSha256(clave_generada);

                string htmlCorreo = "";

                if (UrlPlantillaCorreo != "")
                {
                    UrlPlantillaCorreo = UrlPlantillaCorreo.Replace("[clave]", clave_generada);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlPlantillaCorreo);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream dataStream = response.GetResponseStream())
                        {
                            StreamReader streamReader = null;

                            if (response.CharacterSet == null)
                                streamReader = new StreamReader(dataStream);
                            else
                                streamReader = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                            htmlCorreo = streamReader.ReadToEnd();
                            response.Close();
                            streamReader.Close();
                        }
                    }
                }
                bool correo_enviado = false;

                if (htmlCorreo != "")
                    correo_enviado = await _correoService.EnviarCorreo(correo, "Contraseña restablecida", htmlCorreo);

                if (!correo_enviado)
                    throw new TaskCanceledException("Ups, a ocurrido un problema, intentalo mas tarde");

                bool respuesta = await _repository.Editar(empleado_encontrado);


                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
