using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.Utilidades;
using Sistema.BLL.Servicios.Contrato;
using Sistema.DTO;

namespace Sistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitaController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<CitaDTO>>();

            try
            {
                response.status = true;
                response.data = await _citaService.Lista();
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
        [Route("Crear")]
        public async Task<IActionResult> Crear(CitaDTO citaDto)
        {
            var response = new Response<CitaDTO>();

            try
            {
                response.status = true;
                response.data = await _citaService.Crear(citaDto);
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
        [Route("Aprobar")]
        public async Task<IActionResult> Aprobar([FromBody] int id)
        {
            var response = new Response<bool>();

            try
            {
                response.status = true;
                response.data = await _citaService.Aprobar(id);
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
        [Route("Rechazar")]
        public async Task<IActionResult> Rechazar([FromBody]  int id)
        {
            var response = new Response<bool>();

            try
            {
                response.status = true;
                response.data = await _citaService.Rechazar(id);
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
