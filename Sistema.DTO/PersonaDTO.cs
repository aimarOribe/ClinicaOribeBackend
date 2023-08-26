using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DTO
{
    public class PersonaDTO
    {
        public int IdPersona { get; set; }

        public int? IdUsuario { get; set; }

        public string? Dni { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public string? FechaNacimiento { get; set; }

        public string? Celular { get; set; }

        public string? Genero { get; set; }

        public virtual ICollection<PacienteDTO>? Pacientes { get; set; }

        public virtual ICollection<DoctorDTO>? Doctors { get; set; }
    }
}
