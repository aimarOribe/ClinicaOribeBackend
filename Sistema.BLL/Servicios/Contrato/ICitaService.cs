using Sistema.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.BLL.Servicios.Contrato
{
    public interface ICitaService
    {
        Task<List<CitaDTO>> Lista();
        Task<CitaDTO> Crear(CitaDTO citaDto);
        Task<bool> Reprogramar(string fechaInicio, string fechaFin);
        Task<bool> Aprobar(int id);
        Task<bool> Rechazar(int id);
    }
}
