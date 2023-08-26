using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DAL.Repositorios.Contrato
{
    public interface ICitaRepository
    {
        Task<bool> Aprobar(Cita cita);
        Task<bool> Rechazar(Cita cita);
    }
}
