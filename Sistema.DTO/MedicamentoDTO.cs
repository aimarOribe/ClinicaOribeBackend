using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DTO
{
    public class MedicamentoDTO
    {
        public int IdMedicamento { get; set; }

        public string? Nombre { get; set; }

        public string? Indicaciones { get; set; }

        public string? EfectosSecundarios { get; set; }
    }
}
