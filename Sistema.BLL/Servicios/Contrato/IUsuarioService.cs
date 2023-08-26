using Sistema.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.BLL.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<SesionDTO> ValidarCredenciales(string correo, string clave);
        Task<bool> NuevaClave(int idUsuario, string claveAntigua, string claveNueva);
        Task<bool> ReestablecerClave(string correo);
        Task<UsuarioDTO> Crear(UsuarioDTO usuarioDto);
    }
}
