using Microsoft.AspNetCore.Mvc;
using Versionamento.Application.Interfaces.V1;

namespace Versionamento.API.Controllers.V2
{

    [ApiController, ApiVersion("2")]
    [Route("V{version:api-version}/[controller]")]
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuarioServices _usuariosServices;

        public UsuariosController(IUsuarioServices services)
        {
            _usuariosServices = services;
        }

        [HttpGet]
        public ActionResult GetHour()
        {
            return Ok(DateTime.Now);
        }
    }
}
