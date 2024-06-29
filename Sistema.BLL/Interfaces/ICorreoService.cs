using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.BLL.Interfaces
{
    public interface ICorreoService
    {
        Task<bool> EnviarCorreo(string correoDestino, string asunto, string mensaje);
    }
}
