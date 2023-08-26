using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.Utilidades;
using Sistema.BLL.Servicios.Contrato;
using Sistema.DTO;

namespace Sistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly IEspecialidadService _especialidadServicio;

        public EspecialidadController(IEspecialidadService especialidadServicio)
        {
            _especialidadServicio = especialidadServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<EspecialidadDTO>>();

            try
            {
                response.status = true;
                response.data = await _especialidadServicio.Lista();
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                throw;
            }

            return Ok(response);
        }
    }
}
