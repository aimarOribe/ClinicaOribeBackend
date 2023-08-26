using System;
using System.Collections.Generic;

namespace Sistema.Model;

public partial class Doctor
{
    public int IdDoctor { get; set; }

    public int? IdPersona { get; set; }

    public int? IdEspecialidad { get; set; }

    public DateTime? HorarioInicio { get; set; }

    public DateTime? HorarioFin { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Especialidad? IdEspecialidadNavigation { get; set; }

    public virtual Persona? IdPersonaNavigation { get; set; }
}
