using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public int? IdRol { get; set; }

        public string? RolDescripcion { get; set; }

        public string? NombreUsuario { get; set; }

        public string? Correo { get; set; }

        public string? Clave { get; set; }

        public int? Activo { get; set; }

        public virtual ICollection<PersonaDTO> Personas { get; set; }
    }
}
