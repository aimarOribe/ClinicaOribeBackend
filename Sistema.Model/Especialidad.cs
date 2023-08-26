using System;
using System.Collections.Generic;

namespace Sistema.Model;

public partial class Especialidad
{
    public int IdEspecialidad { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
