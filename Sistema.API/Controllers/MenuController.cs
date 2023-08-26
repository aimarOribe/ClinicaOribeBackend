using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.Utilidades;
using Sistema.BLL.Servicios.Contrato;
using Sistema.DTO;

namespace Sistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuServicio;

        public MenuController(IMenuService menuServicio)
        {
            _menuServicio = menuServicio;
        }

        [HttpGet]
        [Route("Lista/{idUsuario:int}")]
        public async Task<IActionResult> Lista(int idUsuario)
        {
            var response = new Response<List<MenuDTO>>();

            try
            {
                response.status = true;
                response.data = await _menuServicio.Lista(idUsuario);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.msg = ex.Message;
                throw ex;
            }

            return Ok(response);
        }
    }
}
