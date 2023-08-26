using System;
using System.Collections.Generic;

namespace Sistema.Model;

public partial class Historialcita
{
    public int IdHistorialCita { get; set; }

    public int? IdCita { get; set; }

    public string? Diagnostico { get; set; }

    public string? Tratamiento { get; set; }

    public virtual Cita? IdCitaNavigation { get; set; }
}
