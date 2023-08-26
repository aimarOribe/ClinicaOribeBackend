using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DTO
{
    public class DoctorDTO
    {
        public int IdDoctor { get; set; }

        public int? IdPersona { get; set; }

        public int? IdEspecialidad { get; set; }

        public string? EspecialidadNombre { get; set; }

        public string? HorarioInicio { get; set; }

        public string? HorarioFin { get; set; }
    }
}
