using Sistema.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.BLL.Servicios.Contrato
{
    public interface IEspecialidadService
    {
        Task<List<EspecialidadDTO>> Lista();
    }
}
