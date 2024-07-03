using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Sistema.AppWeb.Models.ViewModels;
using Sistema.AppWeb.Utilidades.Response;
using Sistema.BLL.Interfaces;
using SistemaEntidades;

namespace SistemaRentaCarAppWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuarioService;
        private readonly IRolService _rolService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuario usuarioService, IRolService rolService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaRoles()
        {
            List<VMRol> vmlistRoles = _mapper.Map<List<VMRol>>(await _rolService.Lista());
            return StatusCode(StatusCodes.Status200OK, vmlistRoles);
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMEmpleado> vmlistEmpleados = _mapper.Map<List<VMEmpleado>>(await _usuarioService.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmlistEmpleados });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            GenericResponse<VMEmpleado> gResponse = new GenericResponse<VMEmpleado>();

            try
            {
                VMEmpleado vmEmpleado = JsonConvert.DeserializeObject<VMEmpleado>(modelo);
                string nombreFoto = "";
                Stream fotoStream = null;

                if (foto != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(foto.FileName);
                    nombreFoto = String.Concat(nombre_en_codigo, extension);
                    fotoStream = foto.OpenReadStream();
                }

                string urlPlantillaCorreo = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/EnviarClave?correo=[correo]&clave=[clave]";

                Empleado empleadoCreado = await _usuarioService.Crear(_mapper.Map<Empleado>(vmEmpleado), fotoStream, nombreFoto, urlPlantillaCorreo);
            
                vmEmpleado = _mapper.Map<VMEmpleado>(empleadoCreado);

                gResponse.Estado = true;
                gResponse.Objeto = vmEmpleado;
            }
            catch(Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            GenericResponse<VMEmpleado> gResponse = new GenericResponse<VMEmpleado>();

            try
            {
                VMEmpleado vmEmpleado = JsonConvert.DeserializeObject<VMEmpleado>(modelo);
                string nombreFoto = "";
                Stream fotoStream = null;

                if (foto != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(foto.FileName);
                    nombreFoto = String.Concat(nombre_en_codigo, extension);
                    fotoStream = foto.OpenReadStream();
                }


                Empleado empleadoEditado = await _usuarioService.Editar(_mapper.Map<Empleado>(vmEmpleado), fotoStream, nombreFoto);

                vmEmpleado = _mapper.Map<VMEmpleado>(empleadoEditado);

                gResponse.Estado = true;
                gResponse.Objeto = vmEmpleado;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdUsuario)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Estado = await _usuarioService.Eliminar(IdUsuario);
            }
            catch(Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

    }
}
