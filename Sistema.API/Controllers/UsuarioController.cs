using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.Utilidades;
using Sistema.BLL.Servicios.Contrato;
using Sistema.BLL.Servicios.Implementacion;
using Sistema.DTO;

namespace Sistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login)
        {
            var response = new Response<SesionDTO>();

            try
            {
                response.status = true;
                response.data = await _usuarioService.ValidarCredenciales(login.Correo, login.Clave);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("ReestablecerClave")]
        public async Task<IActionResult> ReestablecerClave([FromBody] ForgotPasswordDTO forgotPasswordDto)
        {
            var response = new Response<bool>();

            try
            {
                response.status = true;
                response.data = await _usuarioService.ReestablecerClave(forgotPasswordDto.correo);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("NuevaClave")]
        public async Task<IActionResult> CambiarClave([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            var response = new Response<bool>();

            try
            {
                response.status = true;
                response.data = await _usuarioService.NuevaClave(resetPasswordDto.idUsuario, resetPasswordDto.claveAntigua, resetPasswordDto.claveNueva);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuarioDTO)
        {
            var response = new Response<UsuarioDTO>();

            try
            {
                response.status = true;              
                response.data = await _usuarioService.Crear(usuarioDTO);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }
    }
}
