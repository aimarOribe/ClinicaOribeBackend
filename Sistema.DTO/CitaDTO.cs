using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DTO
{
    public class CitaDTO
    {
        public int IdCita { get; set; }

        public int? IdPaciente { get; set; }

        public string? DniPaciente { get; set; }

        public string? NombrePaciente { get; set; }

        public string? ApellidoPaciente { get; set; }

        public int? IdDoctor { get; set; }

        public string? NombreDoctor { get; set; }

        public string? ApellidoDoctor { get; set; }

        public string? NombreEspecialidad { get; set; }

        public string? FechaInicio { get; set; }

        public string? FechaFin { get; set; }

        public string? Descripcion { get; set; }

        public int? Estado { get; set; }
    }
}
