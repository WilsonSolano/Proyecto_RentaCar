using Sistema.DAL.Interfaces;
using Sistema.DAL.DBContext;
using SistemaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sistema.DAL.Implementacion
{
    public class RentaRepository : GenericRepository<Renta>, IRentasRepository
    {
        private readonly CUSERSALIENDESKTOPPROYECTOSPROYECTO_RENTACARSISTEMAENTIDADESRENTACARMDFContext _dbContext;

        public RentaRepository(CUSERSALIENDESKTOPPROYECTOSPROYECTO_RENTACARSISTEMAENTIDADESRENTACARMDFContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Renta> Registrar(Renta entidad)
        {
            Renta rentaGenerada = new Renta();

            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Vehiculo vehiculoSolicitado = _dbContext.Vehiculos.Where(p => p.IdVehiculo == entidad.IdVehiculo).First();
                    vehiculoSolicitado.Disponible = 0;
                    _dbContext.Vehiculos.Update(vehiculoSolicitado);

                    await _dbContext.SaveChangesAsync();

                    rentaGenerada = entidad;

                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return rentaGenerada;
        }

        public async Task<List<Factura>> Reporte(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Factura> listaResumen = await _dbContext.Facturas.Include(r => r.IdRentaNavigation).
                ThenInclude(u => u.IdEmpleadoNavigation).
                Include(r => r.IdRentaNavigation).
                Where(f => f.FechaEmision.Date >= fechaInicio.Date &&
                f.FechaEmision.Date <= fechaInicio.Date).ToListAsync();
            
            return listaResumen;
        }
    }
}
