using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.BLL.Interfaces
{
    public interface IFireBaseService
    {
        Task<string> subirStorage(Stream streamArchivo, string carpetaDestino,string nombreArchivo);
        Task<bool> eliminarStorage(string carpetaDestino, string nombreArchivo);
    }
}
