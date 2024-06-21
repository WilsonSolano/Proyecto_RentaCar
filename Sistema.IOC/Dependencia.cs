﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sistema.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Sistema.DAL.Implementacion;
using Sistema.DAL.Interfaces;
using Sistema.BLL.Implementacion;
using Sistema.BLL.Interfaces;


namespace Sistema.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<CUSERSALIENDESKTOPPROYECTOSPROYECTO_RENTACARSISTEMAENTIDADESRENTACARMDFContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("cadenaSQL"));
            });
        }
    }
}