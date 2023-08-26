using Sistema.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.BLL.Servicios.Contrato
{
    public interface IPacienteService
    {
        Task<List<UsuarioDTO>> Lista();
        Task<List<UsuarioDTO>> ListaActivos();
        Task<UsuarioDTO> BuscarPorDni(string dni);
        Task<UsuarioDTO> ObtenerById(int idUsuario);
        Task<bool> Editar(UsuarioDTO usuarioDto);
        Task<bool> Activar(int id);
        Task<bool> Desactivar(int id);
        Task<bool> Eliminar(int id);
    }
}
