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
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<UsuarioDTO>>();

            try
            {
                response.status = true;
                response.data = await _doctorService.Lista();
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
                response.data = await _doctorService.ListaActivos();
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
        [Route("ListaPorEspecialidadyActivos")]
        public async Task<IActionResult> ListaPorEspecialidadyActivos([FromBody] int idEspecialidad)
        {
            var response = new Response<List<UsuarioDTO>>();

            try
            {
                response.status = true;
                response.data = await _doctorService.ListaPorEspecialidadyActivos(idEspecialidad);
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
        [Route("{idDoctor:int}")]
        public async Task<IActionResult> ById(int idDoctor)
        {
            var response = new Response<UsuarioDTO>();

            try
            {
                response.status = true;
                response.data = await _doctorService.ObtenerById(idDoctor);
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
                response.data = await _doctorService.Editar(usuarioDto);
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
                response.data = await _doctorService.Activar(id);
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
                response.data = await _doctorService.Desactivar(id);
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
                response.data = await _doctorService.Eliminar(id);
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
