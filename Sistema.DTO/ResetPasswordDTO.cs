using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DTO
{
    public class ResetPasswordDTO
    {
        public int idUsuario { get; set; }

        public string claveAntigua { get; set; }

        public string claveNueva { get; set; }
    }
}
