using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema.BLL.Recursos;
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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepositorio;
        private readonly IGenericRepository<Usuario> _usuarioRepositorioCrud;
        private readonly IGenericRepository<Persona> _personaRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepositorio, IGenericRepository<Usuario> usuarioRepositorioCrud, IGenericRepository<Persona> personaRepository, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _usuarioRepositorioCrud = usuarioRepositorioCrud;
            _personaRepository = personaRepository;
            _mapper = mapper;
        }

        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                string claveEncriptada = RecursosService.ConvertirSha256(clave);

                var queryUsuario = await _usuarioRepositorio.Consultar(u => u.Correo == correo && u.Clave == claveEncriptada);
                if (queryUsuario.FirstOrDefault() == null)
                {
                    throw new Exception("Credenciales Invalidas");
                }
                else
                {
                    Usuario devolverUsuario = queryUsuario.Include(u => u.IdRolNavigation).First();
                    return _mapper.Map<SesionDTO>(devolverUsuario);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ReestablecerClave(string correo)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.Correo == correo);
                if (usuarioEncontrado == null) { throw new Exception("El correo no existe"); }

                string nuevaClave = RecursosService.GenerarClave();

                string asunto = "Cuenta Reestablecida en OTienda";
                string mensaje = "<h3>Se cuenta fue reestablecida correctamente</h3></br><p>Su nueva contraseña de acceso es !clave!</p>";
                mensaje = mensaje.Replace("!clave!", nuevaClave);
                bool respuesta = RecursosService.EnviarCorreo(usuarioEncontrado.Correo, asunto, mensaje);

                if (respuesta)
                {
                    usuarioEncontrado.Clave = RecursosService.ConvertirSha256(nuevaClave);
                    usuarioEncontrado.Reestablecer = true;
                    var usuarioModificado = await _usuarioRepositorio.Editar(usuarioEncontrado);
                    if (usuarioModificado)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Ocurrio un error en el reestableciento de la contraseña");
                    }
                }
                else
                {
                    throw new Exception("Ocurrio un error en el envio del correo de reestablecimiento de su contraseña");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> NuevaClave(int idUsuario, string claveAntigua, string claveNueva)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == idUsuario);
                if (usuarioEncontrado == null) { throw new Exception("El usuario no existe"); }


                if (usuarioEncontrado.Clave != RecursosService.ConvertirSha256(claveAntigua)) throw new Exception("Contraseña Antigua Incorrecta");

                usuarioEncontrado.Clave = RecursosService.ConvertirSha256(claveNueva);

                usuarioEncontrado.Reestablecer = false;

                bool respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);

                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO usuarioDto)
        {
            var verificacionCorreo = await _usuarioRepositorio.Consultar(u => u.Correo == usuarioDto.Correo);
            if(verificacionCorreo.Count() != 0)
            {
                throw new Exception("Este correo ya se encuentra en uso");
            }

            var verificacionNombreUsuario = await _usuarioRepositorio.Consultar(u => u.NombreUsuario == usuarioDto.NombreUsuario);
            if (verificacionNombreUsuario.Count() != 0)
            {
                throw new Exception("Este nombre de usuario ya se encuentra en uso");
            }

            var verificacionDni = await _personaRepository.Consultar(p => p.Dni == usuarioDto.Personas.First().Dni);
            if(verificacionDni.Count() != 0)
            {
                throw new Exception("Este DNI ya se encuentra en uso");
            }

            string clave = RecursosService.GenerarClave();
            string asunto = "Creacion de Cuenta en OTienda";
            string mensaje = "<h3>Se cuenta fue creada correctamente</h3></br><p>Su contraseña de acceso es !clave!</p>";
            mensaje = mensaje.Replace("!clave!", clave);

            bool respuesta = RecursosService.EnviarCorreo(usuarioDto.Correo, asunto, mensaje);
            if (respuesta)
            {
                usuarioDto.Clave = RecursosService.ConvertirSha256(clave);

                var usuarioCreado = await _usuarioRepositorio.Crear(_mapper.Map<Usuario>(usuarioDto));
                if (usuarioCreado.IdUsuario == 0)
                {
                    throw new Exception("No se pudo crear");
                }
                var query = await _usuarioRepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);

                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            }
            else
            {
                throw new Exception("No se pudo enviar el correo, vuelva a intentarlo otra vez");
            }  
        }
    }
}
