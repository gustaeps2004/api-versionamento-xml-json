using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<Object>> GetAll()
        {
            try
            {
                string contentType = Request.Headers.ContentType.ToString();

                var usuarios = await _services.GetAll(contentType);
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
        public async Task<ActionResult<Object>> GetAll(Guid codigo)
        {
            try
            {
                string contentType = Request.Headers.ContentType.ToString();

                var usuario = await _services.GetByCodigo(codigo, contentType);
                if (usuario is null)
                    return BadRequest();

                return Ok(contentType != "application/xml" ? usuario : usuario.ToString());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("CriarUsuario")]
        public async Task<ActionResult> CriarUsuario()
        {
            try
            {
                string contentType = Request.Headers.ContentType.ToString();
                using (var reader = new StreamReader(Request.Body))
                {
                    await _services.CriarUsuario(await reader.ReadToEndAsync(), contentType);
                }                                               

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletarUsuario/{codigo:Guid}")]
        public async Task<ActionResult> DeletarUsuario(Guid codigo)
        {

        }
    }
}
