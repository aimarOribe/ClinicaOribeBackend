using System;
using System.Collections.Generic;

namespace Sistema.Model;

public partial class Pago
{
    public int IdPago { get; set; }

    public int? IdCita { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? FechaPago { get; set; }

    public int? Estado { get; set; }

    public virtual Cita? IdCitaNavigation { get; set; }
}
