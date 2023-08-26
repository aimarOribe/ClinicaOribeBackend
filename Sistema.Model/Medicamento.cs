using System;
using System.Collections.Generic;

namespace Sistema.Model;

public partial class Medicamento
{
    public int IdMedicamento { get; set; }

    public string? Nombre { get; set; }

    public string? Indicaciones { get; set; }

    public string? EfectosSecundarios { get; set; }

    public virtual ICollection<Medicamentocita> Medicamentocita { get; set; } = new List<Medicamentocita>();
}
