using System;
using System.Collections.Generic;

namespace Sistema.Model;

public partial class Medicamentocita
{
    public int IdMedicamentoCita { get; set; }

    public int? IdCita { get; set; }

    public int? IdMedicamento { get; set; }

    public int? Cantidad { get; set; }

    public virtual Cita? IdCitaNavigation { get; set; }

    public virtual Medicamento? IdMedicamentoNavigation { get; set; }
}
