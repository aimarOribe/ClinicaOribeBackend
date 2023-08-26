using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sistema.Model;

namespace Sistema.DAL.DBContext;

public partial class DbcitasmedicasContext : DbContext
{
    public DbcitasmedicasContext()
    {
    }

    public DbcitasmedicasContext(DbContextOptions<DbcitasmedicasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Cita { get; set; }

    public virtual DbSet<Configuracion> Configuracions { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Historialcita> Historialcita { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<Medicamentocita> Medicamentocita { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Menurol> Menurols { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__CITA__394B020267658E24");

            entity.ToTable("CITA");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdDoctor)
                .HasConstraintName("FK__CITA__IdDoctor__59FA5E80");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK__CITA__IdPaciente__59063A47");
        });

        modelBuilder.Entity<Configuracion>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CONFIGURACION");

            entity.Property(e => e.Propiedad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Recurso)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Valor)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PK__DOCTOR__F838DB3E4686BB39");

            entity.ToTable("DOCTOR");

            entity.Property(e => e.HorarioFin).HasColumnType("datetime");
            entity.Property(e => e.HorarioInicio).HasColumnType("datetime");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdEspecialidad)
                .HasConstraintName("FK__DOCTOR__IdEspeci__5629CD9C");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK__DOCTOR__IdPerson__5535A963");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PK__ESPECIAL__693FA0AF1F3937D5");

            entity.ToTable("ESPECIALIDAD");

            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Historialcita>(entity =>
        {
            entity.HasKey(e => e.IdHistorialCita).HasName("PK__HISTORIA__635CD9D24D6C2C2D");

            entity.ToTable("HISTORIALCITA");

            entity.Property(e => e.Diagnostico)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Tratamiento)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.Historialcita)
                .HasForeignKey(d => d.IdCita)
                .HasConstraintName("FK__HISTORIAL__IdCit__6383C8BA");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.IdMedicamento).HasName("PK__MEDICAME__AC96376EE49F1B3A");

            entity.ToTable("MEDICAMENTO");

            entity.Property(e => e.EfectosSecundarios)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Indicaciones)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Medicamentocita>(entity =>
        {
            entity.HasKey(e => e.IdMedicamentoCita).HasName("PK__MEDICAME__9C0F697EF2019462");

            entity.ToTable("MEDICAMENTOCITA");

            entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.Medicamentocita)
                .HasForeignKey(d => d.IdCita)
                .HasConstraintName("FK__MEDICAMEN__IdCit__5FB337D6");

            entity.HasOne(d => d.IdMedicamentoNavigation).WithMany(p => p.Medicamentocita)
                .HasForeignKey(d => d.IdMedicamento)
                .HasConstraintName("FK__MEDICAMEN__IdMed__60A75C0F");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__MENU__4D7EA8E147374235");

            entity.ToTable("MENU");

            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Menurol>(entity =>
        {
            entity.HasKey(e => e.IdMenuRol).HasName("PK__MENUROL__F8D2D5B61558A656");

            entity.ToTable("MENUROL");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Menurols)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__MENUROL__IdMenu__3A81B327");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Menurols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__MENUROL__IdRol__3B75D760");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__PACIENTE__C93DB49BE5C7E53B");

            entity.ToTable("PACIENTE");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK__PACIENTE__IdPers__5070F446");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__PAGO__FC851A3A4BDFF73D");

            entity.ToTable("PAGO");

            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdCita)
                .HasConstraintName("FK__PAGO__IdCita__66603565");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__PERSONA__2EC8D2AC107AB804");

            entity.ToTable("PERSONA");

            entity.Property(e => e.Dni)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Genero)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__PERSONA__IdUsuar__4AB81AF0");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROL__2A49584CE55929CD");

            entity.ToTable("ROL");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF9743EF5DF7");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.Clave)
                .HasMaxLength(555)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Reestablecer).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__USUARIO__IdRol__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
