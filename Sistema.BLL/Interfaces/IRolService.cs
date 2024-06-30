using SistemaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.BLL.Interfaces
{
    public interface IRolService
    {
        Task<List<Rol>> Lista(); 
    }
}
