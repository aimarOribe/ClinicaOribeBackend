using System;
using System.Collections.Generic;

namespace Sistema.Model;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public int? IdPersona { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Persona? IdPersonaNavigation { get; set; }
}
