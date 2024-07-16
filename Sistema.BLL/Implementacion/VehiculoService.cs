using Sistema.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema.DAL.Interfaces;
using SistemaEntidades;

namespace Sistema.BLL.Implementacion
{
    public class VehiculoService : IVehiculosService
    {
        private readonly IGenericRepository<Vehiculo> _repositorio;
        private readonly IFireBaseService _fireBaseService;

        public VehiculoService(IGenericRepository<Vehiculo> repositorio, IFireBaseService fireBaseService)
        {
            _repositorio = repositorio;
            _fireBaseService = fireBaseService;
        }

        public async Task<List<Vehiculo>> Lista()
        {
            IQueryable<Vehiculo> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<Vehiculo> Crear(Vehiculo entidad, Stream imagen = null, string nombreImagen = "")
        {
            Vehiculo vehiculoExiste = await _repositorio.Obtener(v => v.Placas == entidad.Placas);

            if (vehiculoExiste != null)
                throw new TaskCanceledException("La placa a registrar ya se encuentra en la base de datos");

            try
            {
                if(imagen != null)
                {
                    string urlImagen = await _fireBaseService.subirStorage(imagen,"carpeta_vehiculo", nombreImagen);
                    entidad.UrlImagen = urlImagen;
                }

                Vehiculo vehiculoCreado = await _repositorio.Crear(entidad);

                if(vehiculoCreado.IdVehiculo == 0)
                    throw new TaskCanceledException("Las placas ya se encuentran registradas");

                return vehiculoCreado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Vehiculo> Editar(Vehiculo entidad, Stream imagen = null)
        {
            Vehiculo vehiculoExiste = await _repositorio.Obtener(p => p.Placas == entidad.Placas);

            if(vehiculoExiste != null)
                throw new TaskCanceledException("Las placas ya se encuentran registradas");

            try
            {
                IQueryable<Vehiculo> queryVehiculo = await _repositorio.Consultar(v => v.IdVehiculo == entidad.IdVehiculo);

                Vehiculo vehiculoEditar = queryVehiculo.First();

                vehiculoEditar.Marca = entidad.Marca;
                vehiculoEditar.Modelo = entidad.Modelo;
                vehiculoEditar.Anio = entidad.Anio;
                vehiculoEditar.Descripcion = entidad.Descripcion;
                vehiculoEditar.PrecioRenta = entidad.PrecioRenta;
                vehiculoEditar.Disponible = entidad.Disponible;
                
                if(imagen != null)
                {
                    string urlImagen = await _fireBaseService.subirStorage(imagen, "carpeta_vehiculo", vehiculoEditar.NombreImagen);
                    vehiculoEditar.UrlImagen = urlImagen;
                }

                bool respuesta = await _repositorio.Editar(vehiculoEditar);
                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el vehiculo");

                return vehiculoEditar;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idVehiculo)
        {
            try
            {
                Vehiculo vehiculoEncontrado = await _repositorio.Obtener(v => v.IdVehiculo == idVehiculo);

                if(vehiculoEncontrado == null)
                    throw new TaskCanceledException("El Vehiculo no existe");

                string nombreImagen = vehiculoEncontrado.NombreImagen;

                bool respuesta = await _repositorio.Eliminar(vehiculoEncontrado);
                if (respuesta)
                    await _fireBaseService.eliminarStorage("carpeta_vehiculo", nombreImagen);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
