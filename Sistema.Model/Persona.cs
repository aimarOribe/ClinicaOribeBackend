using System;
using System.Collections.Generic;

namespace Sistema.Model;

public partial class Persona
{
    public int IdPersona { get; set; }

    public int? IdUsuario { get; set; }

    public string? Dni { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Celular { get; set; }

    public string? Genero { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
