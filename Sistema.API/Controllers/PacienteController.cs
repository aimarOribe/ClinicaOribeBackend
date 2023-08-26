using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.BLL.Servicios.Contrato;
using Sistema.DTO;
using Sistema.API.Utilidades;

namespace Sistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<UsuarioDTO>>();

            try
            {
                response.status = true;
                response.data = await _pacienteService.Lista();
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("ListaActivos")]
        public async Task<IActionResult> ListaActivos()
        {
            var response = new Response<List<UsuarioDTO>>();

            try
            {
                response.status = true;
                response.data = await _pacienteService.ListaActivos();
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("BuscarPorDni/{dni}")]
        public async Task<IActionResult> BuscarPorDni(string dni)
        {
            var response = new Response<UsuarioDTO>();

            try
            {
                response.status = true;
                response.data = await _pacienteService.BuscarPorDni(dni);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{idPaciente:int}")]
        public async Task<IActionResult> ById(int idPaciente)
        {
            var response = new Response<UsuarioDTO>();

            try
            {
                response.status = true;
                response.data = await _pacienteService.ObtenerById(idPaciente);
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
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuarioDto)
        {
            var response = new Response<bool>();

            try
            {
                response.status = true;
                response.data = await _pacienteService.Editar(usuarioDto);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("Activar/{id:int}")]
        public async Task<IActionResult> Activar(int id)
        {
            var response = new Response<bool>();

            try
            {
                response.status = true;
                response.data = await _pacienteService.Activar(id);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("Desactivar/{id:int}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var response = new Response<bool>();

            try
            {
                response.status = true;
                response.data = await _pacienteService.Desactivar(id);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = new Response<bool>();

            try
            {
                response.status = true;
                response.data = await _pacienteService.Eliminar(id);
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
