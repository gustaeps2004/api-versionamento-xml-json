using Microsoft.AspNetCore.Mvc;
using Versionamento.Application.Interfaces.V2;

namespace Versionamento.API.Controllers.V2
{

    [ApiController, ApiVersion("2")]
    [Route("v{version:apiVersion}/[controller]")]
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuarioServices _usuariosServices;

        public UsuariosController(IUsuarioServices services)
        {
            _usuariosServices = services;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<object>> GetAll()
        {
            try
            {
                string contentType = Request.Headers.ContentType.ToString();

                var usuarios = await _usuariosServices.GetAll(contentType);
                if (usuarios is null)
                    return BadRequest();

                return Ok(usuarios);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetByCodigo/{codigo:Guid}")]
        public async Task<ActionResult<object>> GetAll(Guid codigo)
        {
            try
            {
                string contentType = Request.Headers.ContentType.ToString();

                var usuario = await _usuariosServices.GetByCodigo(codigo, contentType);
                if (usuario is null)
                    return BadRequest();

                return Ok(contentType != "application/xml" ? usuario : usuario.ToString());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
