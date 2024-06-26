using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaEntidades;

namespace Sistema.DAL.Interfaces
{
    public interface IRentasRepository : IGenericRepository<Renta>
    {
        Task<Renta> Registrar(Renta entidad);
        Task<List<Factura>> Reporte(DateTime fechaInicio, DateTime fechaFin);
    }
}
