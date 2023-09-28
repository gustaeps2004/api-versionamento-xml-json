using Microsoft.AspNetCore.Mvc;
using Versionamento.Application.DTOs;
using Versionamento.Application.Interfaces;

namespace Versionamento.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioServices _services;

        public UsuariosController(IUsuarioServices services)
        {
            _services = services;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UsuariosDto>>> GetAll()
        {
            try
            {

                var usuarios = await _services.GetAll();
                if (usuarios is null)
                    return BadRequest();

                return Ok(usuarios);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
