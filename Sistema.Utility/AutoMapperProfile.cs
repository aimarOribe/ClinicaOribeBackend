using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Sistema.DTO;
using Sistema.Model;

namespace Sistema.Utility
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.Activo,
                    opt => opt.MapFrom(origen => origen.Activo == true ? 1 : 0)
                );
            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.NombreUsuario,
                    opt => opt.MapFrom(origen => origen.NombreUsuario)
                )
                .ForMember(destino =>
                    destino.Reestablecer,
                    opt => opt.MapFrom(origen => origen.Reestablecer == true ? 1 : 0)
                );
            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.Activo,
                    opt => opt.MapFrom(origen => origen.Activo == 1 ? true : false)
                );
            CreateMap<SesionDTO, Usuario>()
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.NombreUsuario,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.Reestablecer,
                    opt => opt.MapFrom(origen => origen.Reestablecer == 1 ? true : false)
                );
            #endregion Usuario

            #region Persona
            CreateMap<Persona, PersonaDTO>()
                .ForMember(destino =>
                    destino.FechaNacimiento,
                    opt => opt.MapFrom(origen => origen.FechaNacimiento.Value.Date.ToString("dd/MM/yyyy"))
                );
            CreateMap<PersonaDTO, Persona>()
                .ForMember(destino =>
                    destino.FechaNacimiento,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                );
            #endregion Persona

            #region Paciente
            CreateMap<Paciente, PacienteDTO>().ReverseMap();
            #endregion Paciente

            #region Doctor
            CreateMap<Doctor, DoctorDTO>()
                .ForMember(destino =>
                    destino.EspecialidadNombre,
                    opt => opt.MapFrom(origen => origen.IdEspecialidadNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.HorarioInicio,
                    opt => opt.MapFrom(origen => origen.HorarioInicio.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.HorarioFin,
                    opt => opt.MapFrom(origen => origen.HorarioFin.Value.ToString("dd/MM/yyyy"))
                );
            CreateMap<DoctorDTO, Doctor>()
                .ForMember(destino =>
                    destino.IdEspecialidadNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.HorarioInicio,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.HorarioInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                )
                .ForMember(destino =>
                    destino.HorarioFin,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.HorarioFin, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                ); ;
            #endregion Doctor

            #region Cita
            CreateMap<Cita, CitaDTO>()
                .ForMember(destino =>
                    destino.DniPaciente,
                    opt => opt.MapFrom(origen => origen.IdPacienteNavigation.IdPersonaNavigation.Dni)
                )
                .ForMember(destino =>
                    destino.NombrePaciente,
                    opt => opt.MapFrom(origen => origen.IdPacienteNavigation.IdPersonaNavigation.Nombres)
                )
                .ForMember(destino =>
                    destino.ApellidoPaciente,
                    opt => opt.MapFrom(origen => origen.IdPacienteNavigation.IdPersonaNavigation.Apellidos)
                )
                .ForMember(destino =>
                    destino.NombreDoctor,
                    opt => opt.MapFrom(origen => origen.IdDoctorNavigation.IdPersonaNavigation.Nombres)
                )
                .ForMember(destino =>
                    destino.ApellidoDoctor,
                    opt => opt.MapFrom(origen => origen.IdDoctorNavigation.IdPersonaNavigation.Apellidos)
                )
                .ForMember(destino =>
                    destino.NombreEspecialidad,
                    opt => opt.MapFrom(origen => origen.IdDoctorNavigation.IdEspecialidadNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.FechaInicio,
                    opt => opt.MapFrom(origen => origen.FechaInicio.Value.ToString("dd-MM-yyyy HH:mm"))
                )
                .ForMember(destino =>
                    destino.FechaFin,
                    opt => opt.MapFrom(origen => origen.FechaFin.Value.ToString("dd-MM-yyyy HH:mm"))
                );
            CreateMap<CitaDTO, Cita>()
                .ForMember(destino =>
                    destino.IdPacienteNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.IdDoctorNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.FechaInicio,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaInicio, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture))
                )
                .ForMember(destino =>
                    destino.FechaFin,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaFin, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture))
                );
            #endregion Cita

            #region Especialidad
            CreateMap<Especialidad, EspecialidadDTO>().ReverseMap();
            #endregion Especialidad

            #region Historial Cita
            CreateMap<Historialcita, Historialcita>().ReverseMap();
            #endregion Historial Cita

            #region Medicamento Cita
            CreateMap<Medicamentocita, MedicamentocitaDTO>().ReverseMap();
            #endregion Medicamento Cita

            #region Medicamento
            CreateMap<Medicamento, MedicamentoDTO>().ReverseMap();
            #endregion Medicamento

            #region Pago
            CreateMap<Pago, PagoDTO>()
                .ForMember(destino =>
                    destino.Monto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Monto.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.FechaPago,
                    opt => opt.MapFrom(origen => origen.FechaPago.Value.ToString("dd/MM/yyyy"))
                );
            CreateMap<PagoDTO, Pago>()
                .ForMember(destino =>
                    destino.Monto,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Monto, new CultureInfo("es-PE")))
                );
            #endregion Pago
        }
    }
}
