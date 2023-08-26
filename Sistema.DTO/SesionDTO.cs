using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DTO
{
    public class SesionDTO
    {
        public int IdUsuario { get; set; }

        public string? NombreUsuario { get; set; }

        public string? Correo { get; set; }

        public string? RolDescripcion { get; set; }

        public int? Reestablecer { get; set; }
    }
}
