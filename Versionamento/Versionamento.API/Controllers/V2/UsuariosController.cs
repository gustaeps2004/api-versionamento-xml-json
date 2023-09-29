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

        
    }
}
