using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema.BLL.Recursos;
using Sistema.BLL.Servicios.Contrato;
using Sistema.DAL.DBContext;
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
    public class PacienteService: IPacienteService
    {
        private readonly IGenericRepository<Paciente> _pacienteRepositorio;
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IGenericRepository<Persona> _personaRepositorio;
        private readonly IUsuarioService _usuarioService;
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;

        public PacienteService(IGenericRepository<Paciente> pacienteRepositorio, IUsuarioService usuarioService, IPersonaService personaService, IGenericRepository<Usuario> usuarioRepositorio, IGenericRepository<Persona> personaRepositorio, IMapper mapper)
        {
            _pacienteRepositorio = pacienteRepositorio;
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
                var queryPaciente = await _pacienteRepositorio.Consultar();
                var listaPacientes = queryPaciente
                        .Include(per => per.IdPersonaNavigation)
                        .ThenInclude(usu => usu.IdUsuarioNavigation)
                        .ThenInclude(rol => rol.IdRolNavigation)
                        .ToList();

                // Mapeo de Paciente a UsuarioDTO
                var listaUsuarios = listaPacientes.Select(paciente =>
                {
                    var usuarioDto = _mapper.Map<UsuarioDTO>(paciente.IdPersonaNavigation.IdUsuarioNavigation);
                    usuarioDto.Personas = new List<PersonaDTO>
                    {
                        _mapper.Map<PersonaDTO>(paciente.IdPersonaNavigation)
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
                var queryPaciente = await _pacienteRepositorio.Consultar();
                var listaPacientes = queryPaciente
                        .Include(per => per.IdPersonaNavigation)
                        .ThenInclude(usu => usu.IdUsuarioNavigation)
                        .ThenInclude(rol => rol.IdRolNavigation)
                        .Where(u => u.IdPersonaNavigation.IdUsuarioNavigation.Activo == true)
                        .ToList();

                // Mapeo de Paciente a UsuarioDTO
                var listaUsuarios = listaPacientes.Select(paciente =>
                {
                    var usuarioDto = _mapper.Map<UsuarioDTO>(paciente.IdPersonaNavigation.IdUsuarioNavigation);
                    usuarioDto.Personas = new List<PersonaDTO>
                    {
                        _mapper.Map<PersonaDTO>(paciente.IdPersonaNavigation)
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

        public async Task<UsuarioDTO> BuscarPorDni(string dni)
        {
            try
            {
                var queryPaciente = await _pacienteRepositorio.Consultar(p => p.IdPersonaNavigation.Dni == dni);

                if (queryPaciente.Count() == 0)
                {
                    throw new Exception("Paciente no Encontrado");
                }

                var paciente = queryPaciente
                        .Include(per => per.IdPersonaNavigation)
                        .ThenInclude(usu => usu.IdUsuarioNavigation)
                        .ThenInclude(rol => rol.IdRolNavigation)
                        .First();

                var usuarioDto = _mapper.Map<UsuarioDTO>(paciente.IdPersonaNavigation.IdUsuarioNavigation);
                usuarioDto.Personas = new List<PersonaDTO>
                {
                    _mapper.Map<PersonaDTO>(paciente.IdPersonaNavigation)
                };

                return usuarioDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> ObtenerById(int idPaciente)
        {
            try
            {
                var queryPaciente = await _pacienteRepositorio.Consultar(p => p.IdPaciente == idPaciente);

                if(queryPaciente.Count() == 0)
                {
                    throw new Exception("El paciente no existe");
                }

                var paciente = queryPaciente
                    .Include(u => u.IdPersonaNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation)
                    .ThenInclude(r => r.IdRolNavigation)
                    .First();

                var usuarioDto = _mapper.Map<UsuarioDTO>(paciente.IdPersonaNavigation.IdUsuarioNavigation);
                usuarioDto.Personas = new List<PersonaDTO>
                {
                    _mapper.Map<PersonaDTO>(paciente.IdPersonaNavigation)
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
                var personaModelo = usuarioDto.Personas.First();
                var usuarioModelo = usuarioDto;

                if (personaModelo.Pacientes.Any())
                {
                    var usuarioEncontrado = await _pacienteRepositorio.Consultar(p => p.IdPaciente == personaModelo.Pacientes.First().IdPaciente);

                    if (usuarioEncontrado == null)
                    {
                        throw new Exception("El paciente no existe");
                    }
                    else
                    {
                        var encontrado = usuarioEncontrado.Include(p => p.IdPersonaNavigation)
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
                var queryPaciente = await _pacienteRepositorio.Consultar(p => p.IdPaciente == id);

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
                bool respuestaPaciente = await _pacienteRepositorio.Editar(paciente);

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
                var queryPaciente = await _pacienteRepositorio.Consultar(p => p.IdPaciente == id);

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
                bool respuestaPaciente = await _pacienteRepositorio.Editar(paciente);

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
                var queryPaciente = await _pacienteRepositorio.Consultar(p => p.IdPaciente == id);

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
                bool respuestaPaciente = await _pacienteRepositorio.Editar(paciente);

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
    }
}
