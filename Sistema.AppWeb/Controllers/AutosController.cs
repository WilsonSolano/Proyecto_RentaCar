using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Sistema.AppWeb.Models.ViewModels;
using Sistema.AppWeb.Utilidades.Response;
using Sistema.BLL.Interfaces;
using SistemaEntidades;

namespace SistemaRentaCarAppWeb.Controllers
{
    public class AutosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehiculosService _vehiculosService;

        public AutosController(IMapper mapper, IVehiculosService vehiculosService)
        {
            _mapper = mapper;
            _vehiculosService = vehiculosService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMVehiculo> vmVehiculoLista = _mapper.Map<List<VMVehiculo>>(await _vehiculosService.Lista());

            return StatusCode(StatusCodes.Status200OK, new { data = vmVehiculoLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse<VMVehiculo> gResponse = new GenericResponse<VMVehiculo>();

            try
            {
                VMVehiculo vmVehiculo = JsonConvert.DeserializeObject<VMVehiculo>(modelo);

                string nombreImagen = "";
                Stream imagenStream = null;

                if(imagen != null)
                {
                    string nombreCodigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = String.Concat(nombreCodigo, extension);

                    imagenStream = imagen.OpenReadStream();
                }

                Vehiculo vehiculoCreado = await _vehiculosService.Crear(_mapper.Map<Vehiculo>(vmVehiculo),imagenStream,nombreImagen);

                vmVehiculo = _mapper.Map<VMVehiculo>(vehiculoCreado);

                gResponse.Estado = true;
                gResponse.Objeto = vmVehiculo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse<VMVehiculo> gResponse = new GenericResponse<VMVehiculo>();

            try
            {
                VMVehiculo vmVehiculo = JsonConvert.DeserializeObject<VMVehiculo>(modelo);

                Stream imagenStream = null;

                if (imagen != null)
                {
                    imagenStream = imagen.OpenReadStream();
                }

                Vehiculo vehiculoEditado = await _vehiculosService.Editar(_mapper.Map<Vehiculo>(vmVehiculo), imagenStream);

                vmVehiculo = _mapper.Map<VMVehiculo>(vehiculoEditado);

                gResponse.Estado = true;
                gResponse.Objeto = vmVehiculo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdVehiculo)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _vehiculosService.Eliminar(IdVehiculo);
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
