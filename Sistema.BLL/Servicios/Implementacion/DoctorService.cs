using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema.BLL.Servicios.Contrato;
using Sistema.DAL.Repositorios.Contrato;
using Sistema.DTO;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.BLL.Servicios.Implementacion
{
    public class DoctorService : IDoctorService
    {
        private readonly IGenericRepository<Doctor> _doctorRepositorio;
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IGenericRepository<Persona> _personaRepositorio;
        private readonly IUsuarioService _usuarioService;
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;

        public DoctorService(IGenericRepository<Doctor> doctorRepositorio, IUsuarioService usuarioService, IPersonaService personaService, IGenericRepository<Usuario> usuarioRepositorio, IGenericRepository<Persona> personaRepositorio, IMapper mapper)
        {
            _doctorRepositorio = doctorRepositorio;
            _usuarioService = usuarioService;
            _personaService = personaService;
            _usuarioRepositorio = usuarioRepositorio;
            _personaRepositorio = personaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryDoctor = await _doctorRepositorio.Consultar();
                var listaDoctores = queryDoctor
                        .Include(d => d.IdEspecialidadNavigation)
                        .Include(per => per.IdPersonaNavigation)
                        .ThenInclude(usu => usu.IdUsuarioNavigation)
                        .ThenInclude(rol => rol.IdRolNavigation)
                        .ToList();

                var listaUsuarios = listaDoctores.Select(doctor =>
                {
                    var usuarioDto = _mapper.Map<UsuarioDTO>(doctor.IdPersonaNavigation.IdUsuarioNavigation);
                    usuarioDto.Personas = new List<PersonaDTO>
                    {
                        _mapper.Map<PersonaDTO>(doctor.IdPersonaNavigation)
                    };
                    return usuarioDto;
                }).ToList();

                return listaUsuarios;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UsuarioDTO>> ListaActivos()
        {
            try
            {
                var queryDoctor = await _doctorRepositorio.Consultar();
                var listaDoctores = queryDoctor
                        .Include(d => d.IdEspecialidadNavigation)
                        .Include(per => per.IdPersonaNavigation)
                        .ThenInclude(usu => usu.IdUsuarioNavigation)
                        .ThenInclude(rol => rol.IdRolNavigation)
                        .Where(u => u.IdPersonaNavigation.IdUsuarioNavigation.Activo == true)
                        .ToList();

                var listaUsuarios = listaDoctores.Select(doctor =>
                {
                    var usuarioDto = _mapper.Map<UsuarioDTO>(doctor.IdPersonaNavigation.IdUsuarioNavigation);
                    usuarioDto.Personas = new List<PersonaDTO>
                    {
                        _mapper.Map<PersonaDTO>(doctor.IdPersonaNavigation)
                    };
                    return usuarioDto;
                }).ToList();

                return listaUsuarios;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UsuarioDTO>> ListaPorEspecialidadyActivos(int idEspecialidad)
        {
            try
            {
                var queryDoctor = await _doctorRepositorio.Consultar();
                var listaDoctores = queryDoctor
                        .Include(d => d.IdEspecialidadNavigation)
                        .Include(per => per.IdPersonaNavigation)
                        .ThenInclude(usu => usu.IdUsuarioNavigation)
                        .ThenInclude(rol => rol.IdRolNavigation)
                        .Where(u => u.IdEspecialidad == idEspecialidad && u.IdPersonaNavigation.IdUsuarioNavigation.Activo == true)
                        .ToList();

                var listaUsuarios = listaDoctores.Select(doctor =>
                {
                    var usuarioDto = _mapper.Map<UsuarioDTO>(doctor.IdPersonaNavigation.IdUsuarioNavigation);
                    usuarioDto.Personas = new List<PersonaDTO>
                    {
                        _mapper.Map<PersonaDTO>(doctor.IdPersonaNavigation)
                    };
                    return usuarioDto;
                }).ToList();

                return listaUsuarios;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> ObtenerById(int idDoctor)
        {
            try
            {
                var queryDoctor = await _doctorRepositorio.Consultar(p => p.IdDoctor == idDoctor);

                if (queryDoctor.Count() == 0)
                {
                    throw new Exception("El doctor no existe");
                }

                var doctor = queryDoctor
                    .Include(e => e.IdEspecialidadNavigation)
                    .Include(u => u.IdPersonaNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation)
                    .ThenInclude(r => r.IdRolNavigation)
                    .First();

                var usuarioDto = _mapper.Map<UsuarioDTO>(doctor.IdPersonaNavigation.IdUsuarioNavigation);
                usuarioDto.Personas = new List<PersonaDTO>
                {
                    _mapper.Map<PersonaDTO>(doctor.IdPersonaNavigation)
                };

                return usuarioDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO usuarioDto)
        {
            try
            {
                var verificacionCorreo = await _usuarioRepositorio.Consultar(u => u.Correo == usuarioDto.Correo && u.IdUsuario != usuarioDto.IdUsuario);
                if (verificacionCorreo.Count() != 0)
                {
                    throw new Exception("Este correo ya se encuentra en uso");
                }

                var verificacionNombreUsuario = await _usuarioRepositorio.Consultar(u => u.NombreUsuario == usuarioDto.NombreUsuario && u.IdUsuario != usuarioDto.IdUsuario);
                if (verificacionNombreUsuario.Count() != 0)
                {
                    throw new Exception("Este nombre de usuario ya se encuentra en uso");
                }

                var verificacionDni = await _personaRepositorio.Consultar(p => p.Dni == usuarioDto.Personas.First().Dni && p.IdPersona != usuarioDto.Personas.First().IdPersona);
                if (verificacionDni.Count() != 0)
                {
                    throw new Exception("Este DNI ya se encuentra en uso");
                }

                var personaModelo = usuarioDto.Personas.First();
                var usuarioModelo = usuarioDto;
                var doctorModelo = usuarioDto.Personas.First().Doctors.First();

                if (personaModelo.Doctors.Any())
                {
                    var usuarioEncontrado = await _doctorRepositorio.Consultar(p => p.IdDoctor == personaModelo.Doctors.First().IdDoctor);

                    if (usuarioEncontrado == null)
                    {
                        throw new Exception("El doctor no existe");
                    }
                    else
                    {
                        var encontrado = usuarioEncontrado
                            .Include(e => e.IdEspecialidadNavigation)
                            .Include(p => p.IdPersonaNavigation)
                            .ThenInclude(u => u.IdUsuarioNavigation)
                            .ThenInclude(r => r.IdRolNavigation)
                            .First();

                        encontrado.IdPersonaNavigation.IdUsuarioNavigation.NombreUsuario = usuarioModelo.NombreUsuario;
                        encontrado.IdPersonaNavigation.IdUsuarioNavigation.Correo = usuarioModelo.Correo;

                        encontrado.IdPersonaNavigation.Dni = personaModelo.Dni;
                        encontrado.IdPersonaNavigation.Nombres = personaModelo.Nombres;
                        encontrado.IdPersonaNavigation.Apellidos = personaModelo.Apellidos;
                        encontrado.IdPersonaNavigation.FechaNacimiento = DateTime.ParseExact(personaModelo.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        encontrado.IdPersonaNavigation.Celular = personaModelo.Celular;
                        encontrado.IdPersonaNavigation.Genero = personaModelo.Genero;

                        encontrado.IdEspecialidad = (int)doctorModelo.IdEspecialidad;
                        encontrado.HorarioInicio = DateTime.ParseExact(doctorModelo.HorarioInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        encontrado.HorarioFin = DateTime.ParseExact(doctorModelo.HorarioFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        bool respuestaUsuario = await _usuarioRepositorio.Editar(encontrado.IdPersonaNavigation.IdUsuarioNavigation);
                        if (!respuestaUsuario)
                        {
                            throw new Exception("No se pudo editar");
                        }
                        bool respuestaPesona = await _personaRepositorio.Editar(encontrado.IdPersonaNavigation);
                        if (!respuestaPesona)
                        {
                            throw new Exception("No se pudo editar");
                        }
                        bool respuestaDoctor = await _doctorRepositorio.Editar(encontrado);
                        if (!respuestaDoctor)
                        {
                            throw new Exception("No se pudo editar");
                        }
                        return respuestaPesona;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Activar(int id)
        {
            try
            {
                var queryPaciente = await _doctorRepositorio.Consultar(d => d.IdDoctor == id);

                if (queryPaciente.Count() == 0)
                {
                    throw new Exception("El paciente no existe");
                }

                var paciente = queryPaciente
                    .Include(u => u.IdPersonaNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation)
                    .ThenInclude(r => r.IdRolNavigation)
                    .First();

                paciente.Activo = true;
                bool respuestaPaciente = await _doctorRepositorio.Editar(paciente);

                if (!respuestaPaciente)
                {
                    throw new Exception("No se pudo eliminar");
                }

                var persona = paciente.IdPersonaNavigation;
                var usuario = persona.IdUsuarioNavigation;

                persona.Activo = true;
                usuario.Activo = true;

                bool respuestaPersona = await _personaRepositorio.Editar(persona);
                bool respuestaUsuario = await _usuarioRepositorio.Editar(usuario);

                if (!respuestaPersona || !respuestaUsuario)
                {
                    throw new Exception("No se pudo eliminar");
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Desactivar(int id)
        {
            try
            {
                var queryPaciente = await _doctorRepositorio.Consultar(d => d.IdDoctor == id);

                if (queryPaciente.Count() == 0)
                {
                    throw new Exception("El paciente no existe");
                }

                var paciente = queryPaciente
                    .Include(u => u.IdPersonaNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation)
                    .ThenInclude(r => r.IdRolNavigation)
                    .First();

                paciente.Activo = false;
                bool respuestaPaciente = await _doctorRepositorio.Editar(paciente);

                if (!respuestaPaciente)
                {
                    throw new Exception("No se pudo eliminar");
                }

                var persona = paciente.IdPersonaNavigation;
                var usuario = persona.IdUsuarioNavigation;

                persona.Activo = false;
                usuario.Activo = false;

                bool respuestaPersona = await _personaRepositorio.Editar(persona);
                bool respuestaUsuario = await _usuarioRepositorio.Editar(usuario);

                if (!respuestaPersona || !respuestaUsuario)
                {
                    throw new Exception("No se pudo eliminar");
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var queryPaciente = await _doctorRepositorio.Consultar(p => p.IdDoctor == id);

                if (queryPaciente.Count() == 0)
                {
                    throw new Exception("El doctor no existe");
                }

                var paciente = queryPaciente
                    .Include(u => u.IdPersonaNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation)
                    .ThenInclude(r => r.IdRolNavigation)
                    .First();

                bool respuestaPaciente = await _doctorRepositorio.Eliminar(paciente);

                if (!respuestaPaciente)
                {
                    throw new Exception("No se pudo eliminar");
                }

                var persona = paciente.IdPersonaNavigation;
                var usuario = persona.IdUsuarioNavigation;

                bool respuestaPersona = await _personaRepositorio.Eliminar(persona);
                bool respuestaUsuario = await _usuarioRepositorio.Eliminar(usuario);

                if (!respuestaPersona || !respuestaUsuario)
                {
                    throw new Exception("No se pudo eliminar");
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
