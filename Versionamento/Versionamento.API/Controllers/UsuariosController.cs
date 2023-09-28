using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
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
        public async Task<ActionResult<Object>> GetAll()
        {
            try
            {
                string typeFormat = Request.Headers.Accept.ToString();

                var usuarios = await _services.GetAll(typeFormat);
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
                string accept = Request.Headers.Accept.ToString();

                var usuario = await _services.GetByCodigo(codigo, accept);
                if (usuario is null)
                    return BadRequest();

                return Ok(accept != "application/xml" ? usuario : usuario.ToString());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("CriarUsuario")]
        public async Task<ActionResult> CriarUsuarios()
        {
            try
            {

                string accept = Request.Headers.Accept.ToString();
                await _services.CriarUsuario(Request.Body, accept);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
