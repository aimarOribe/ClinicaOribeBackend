using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DTO
{
    public class MedicamentocitaDTO
    {
        public int IdMedicamentoCita { get; set; }

        public int? IdCita { get; set; }

        public int? IdMedicamento { get; set; }

        public string? MedicamentoNombre { get; set; }

        public int? Cantidad { get; set; }
    }
}
