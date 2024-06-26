using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.DAL.DBContext;
using Sistema.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Sistema.DAL.Implementacion
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly CUSERSALIENDESKTOPPROYECTOSPROYECTO_RENTACARSISTEMAENTIDADESRENTACARMDFContext _dBContext;

        public GenericRepository(CUSERSALIENDESKTOPPROYECTOSPROYECTO_RENTACARSISTEMAENTIDADESRENTACARMDFContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                TEntity entidad = await _dBContext.Set<TEntity>().FirstOrDefaultAsync(filtro);
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TEntity> Crear(TEntity entidad)
        {
            try
            {
                _dBContext.Set<TEntity>().Add(entidad);
                await _dBContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(TEntity entidad)
        {
            try
            {
                _dBContext.Set<TEntity>().Update(entidad);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TEntity entidad)
        {
            try
            {
                _dBContext.Set<TEntity>().Remove(entidad);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null)
        {
            IQueryable<TEntity> queryEntidad = filtro == null ? _dBContext.Set<TEntity>() : _dBContext.Set<TEntity>().Where(filtro);
            return queryEntidad;
        }
        
    }
}
