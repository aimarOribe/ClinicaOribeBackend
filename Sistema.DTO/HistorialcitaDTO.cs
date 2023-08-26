using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DTO
{
    public class HistorialcitaDTO
    {
        public int IdHistorialCita { get; set; }

        public int? IdCita { get; set; }

        public string? Diagnostico { get; set; }

        public string? Tratamiento { get; set; }
    }
}
