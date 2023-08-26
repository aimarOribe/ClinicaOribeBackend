using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DTO
{
    public class PagoDTO
    {
        public int IdPago { get; set; }

        public int? IdCita { get; set; }

        public string? Monto { get; set; }

        public string? FechaPago { get; set; }

        public int? Estado { get; set; }
    }
}
