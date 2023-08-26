using System;
using System.Collections.Generic;

namespace Sistema.Model;

public partial class Cita
{
    public int IdCita { get; set; }

    public int? IdPaciente { get; set; }

    public int? IdDoctor { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string? Descripcion { get; set; }

    public int? Estado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Historialcita> Historialcita { get; set; } = new List<Historialcita>();

    public virtual Doctor? IdDoctorNavigation { get; set; }

    public virtual Paciente? IdPacienteNavigation { get; set; }

    public virtual ICollection<Medicamentocita> Medicamentocita { get; set; } = new List<Medicamentocita>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
